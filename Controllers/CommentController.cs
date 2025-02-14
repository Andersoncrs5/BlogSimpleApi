using BlogSimpleApi.Models;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CommentController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(long id)
        {
            try
            {
                Comment comment = await this._uow.CommentRepository.GetAsync(id);

                if (comment is null)
                    return NotFound("Comment not found");

                return StatusCode(StatusCodes.Status302Found, comment);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(long id)
        {
            try
            {
                Comment comment = await this._uow.CommentRepository.GetAsync(id);

                if (comment is null)
                    return NotFound("Comment not found");

                await this._uow.CommentRepository.DeleteAsync(comment);

                await this._uow.CommitAsync();
                return Ok(comment);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Create([FromBody] Comment comment)
        {
            try
            {
                User user = await this._uow.UserRepository.GetAsync(comment.UserId);

                if (user == null)
                    return NotFound($"User not found with id : {comment.UserId}");

                comment.EmailUser = user.Email;

                Comment commentCreated = await this._uow.CommentRepository.CreateAsync(comment);
                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, commentCreated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpGet("{id}/GetAllPostAsync")]
        public async Task<ActionResult<List<Comment>>> GetAllPostAsync(long id)
        {
            try
            {
                List<Comment> comment = await this._uow.CommentRepository.GetAllPostAsync(id);

                return StatusCode(StatusCodes.Status302Found, comment);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Comment>> Update([FromBody] Comment comment)
        {
            try
            {
                Comment commentUpdated = await this._uow.CommentRepository.UpdateAsync(comment);
                await this._uow.CommitAsync();
                return Ok(commentUpdated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }
    }
}
