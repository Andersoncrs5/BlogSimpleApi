using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PostController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {
            try
            {
                List<Post> posts = await _uow.PostRepository.GetAllAsync();
                return Ok(posts);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(long id)
        {
            try
            {
                Post post = await _uow.PostRepository.GetAsync(id);

                if (post == null) return NotFound("Post not found");

                return Ok(post); 
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post post)
        {
            try
            {
                User user = await this._uow.UserRepository.GetAsync(post.UserId);

                if (user == null)
                    return NotFound($"User not found with id : {post.UserId}");

                post.EmailUser = user.Email;

                var createdPost = await _uow.PostRepository.CreateAsync(post);

                await _uow.CommitAsync();
                return CreatedAtAction(nameof(Get), new { id = createdPost.Id }, createdPost);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Update(long id,[FromBody] Post post)
        {
            try
            {
                if (id != post.Id) return BadRequest("IDs do not match");

                Post updatedPost = await _uow.PostRepository.UpdateAsync(post);

                await _uow.CommitAsync();
                return Ok(updatedPost);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                Post post = await _uow.PostRepository.GetAsync(id);
                if (post == null) return NotFound();

                await _uow.FavoritePostRepository.DeleteAllPostsAsync(id);
                await _uow.LikeRepository.DeleteAllPostsAsync(id);
                await _uow.CommentRepository.DeleteAllPostAsync(id);
                await _uow.PostRepository.DeleteAsync(post);
                await _uow.CommitAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }

        }

        [HttpGet("{id}/GetAllComments")]
        public async Task<ActionResult<Post>> GetAllComments(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id is required");

                Post post = await _uow.PostRepository.GetAsync(id);

                if (post == null) return NotFound("Post not found");

                List<Comment> comments = await _uow.CommentRepository.GetAllPostAsync(id);

                return Ok(comments);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
            }
        }

    }
}