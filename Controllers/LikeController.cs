using BlogSimpleApi.Models;
using BlogSimpleApi.SetUnitOfWork;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public LikeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> Create([FromBody] Like like )
        {
            try
            {
                bool check = await this._uow.LikeRepository.ExistsLike(like.UserId,like.PostId);

                if (check == true)
                    return BadRequest("Item already exists");

                User checkUser = await this._uow.UserRepository.GetAsync(like.UserId);
                Post checkPost = await this._uow.PostRepository.GetAsync(like.PostId);

                if (checkUser is null)
                    return NotFound($"Not exists user with this id: {like.UserId} ");

                if (checkPost is null)
                    return NotFound($"Not exists user with this id: {like.PostId} ");

                bool likeCreated = await this._uow.LikeRepository.CreateLike(like);
                this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

        [HttpPost("Exists")]
        public async Task<ActionResult<bool>> Exists([FromBody] Like like)
        {
            try
            {
                return Ok(await this._uow.LikeRepository.ExistsLike(like.UserId, like.PostId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

        [HttpDelete("Delete/{userId}/{postId}")]
        public async Task<ActionResult<bool>> Delete(long userId, long postId)
        {
            try
            {
                bool check = await this._uow.LikeRepository.ExistsLike(userId, postId);

                if (check == false)
                    return BadRequest("Item already exists");

                Like like = await this._uow.LikeRepository.GetAsync(userId, postId);

                if (like == null)
                    return NotFound("Like not found");

                await this._uow.LikeRepository.RemoveLike(like);

                this._uow.CommitAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

        [HttpGet("{idPost}")]
        public async Task<ActionResult<long>> AmountLike(long idPost)
        {
            try
            {
                return await this._uow.LikeRepository.GetAmount(idPost);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }
    }
}