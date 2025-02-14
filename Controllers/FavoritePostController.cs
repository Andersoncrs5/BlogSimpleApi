using BlogSimpleApi.Models;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritePostController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public FavoritePostController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] FavoritePost fp )
        {
            try
            {
                bool check = await this._uow.FavoritePostRepository.ExistAsync(fp);

                if (check == true)
                    return BadRequest("Item already exists");

                User checkUser = await this._uow.UserRepository.GetAsync(fp.UserId);
                Post checkPost = await this._uow.PostRepository.GetAsync(fp.PostId);

                if (checkUser is null)
                    return NotFound($"Not exists user with this id: {fp.UserId} ");

                if (checkPost is null)
                    return NotFound($"Not exists user with this id: {fp.PostId} ");

                bool fpCreated = await this._uow.FavoritePostRepository.CreateAsync(fp);
                this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, true);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

        [HttpPost("Exists")]
        public async Task<ActionResult<bool>> Exists([FromBody] FavoritePost fp)
        {
            try
            {
                return Ok(await this._uow.FavoritePostRepository.ExistAsync(fp));
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
                var fp = await this._uow.FavoritePostRepository.GetAsync(userId, postId);

                if (fp is null)
                    return NotFound("Like not found");

                bool fpUpdated = await this._uow.FavoritePostRepository.DeleteAsync(fp);

                this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

        [HttpGet("{idPost}")]
        public async Task<ActionResult<long>> AmountFavorite(long idPost)
        {
            try
            {
                return await this._uow.FavoritePostRepository.GetAmount(idPost);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error {e} ");
            }
        }

    }
}