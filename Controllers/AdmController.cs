using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogSimpleApi.SetUnitOfWork;
using BlogSimpleApi.Models;
using BlogSimpleApi.Datas;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _c;
        public AdmController(IUnitOfWork uow, AppDbContext c)
        {
            _uow = uow;
            _c = c;
        }

        [HttpGet("/IsAuthorized/{IdUser:long}")]
        public async Task<bool> IsAuthorized(long IdUser)
        {
            try
            {
                User user = await this._uow.UserRepository.GetAsync(IdUser);

                if (user.IsAdm == false)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error {e} ");
            }
        }

        [HttpGet("{IdPost:long}/IsBocked/{IdUser:long}")]
        public async Task<ActionResult<bool>> IsBlocked(long IdPost, long IdUser)
        {
            try
            {
                bool checkAuthorized = await this.IsAuthorized(IdUser);
                if (checkAuthorized == false)
                    return StatusCode(StatusCodes.Status401Unauthorized, $"You are not authorized");

                Post post = await this._uow.PostRepository.GetAsync(IdPost);

                if (post == null)
                    return NotFound("Post not found");

                if (post.IsActived == true)
                    return Ok(false);

                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("{IdPost:long}/UnBlockOrBlockPost/{IdUser:long}")]
        public async Task<ActionResult<bool>> UnBlockOrBlockPost(long IdPost, long IdUser)
        {
            try
            {
                bool checkAuthorized = await this.IsAuthorized(IdUser);
                if (checkAuthorized == false)
                    return StatusCode(StatusCodes.Status401Unauthorized, $"You are not authorized");

                Post post = await this._uow.PostRepository.GetAsync(IdPost);

                if (post == null)
                    return NotFound("Post not found");

                await this._uow.AdmRepository.UnBlockOrBlockPost(post);
                await this._uow.CommitAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("{IdUser:long}/UnBlockOrBlockUser/{IdUserAdm:long}")]
        public async Task<ActionResult<bool>> UnBlockOrBlockUser(long IdUser, long IdUserAdm)
        {
            try
            {
                bool checkAuthorized = await this.IsAuthorized(IdUserAdm);
                if (checkAuthorized == false)
                    return StatusCode(StatusCodes.Status401Unauthorized, $"You are not authorized");

                User user = await this._uow.UserRepository.GetAsync(IdUser);

                if (user == null)
                    return NotFound("Post not found");

                await this._uow.AdmRepository.UnBlockOrBlockUser(user);
                await this._uow.CommitAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("{IdComment:long}/UnBlockOrBlockComment/{IdUserAdm:long}")]
        public async Task<ActionResult<bool>> UnBlockOrBlockComment(long IdComment, long IdUserAdm)
        {
            try
            {
                bool checkAuthorized = await this.IsAuthorized(IdUserAdm);
                if (checkAuthorized == false)
                    return StatusCode(StatusCodes.Status401Unauthorized, $"You are not authorized");

                Comment comment = await this._uow.CommentRepository.GetAsync(IdComment);

                if (comment == null)
                    return NotFound("Post not found");

                await this._uow.AdmRepository.UnBlockOrBlockComment(comment);
                await this._uow.CommitAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("ListOfBlockComments")]
        public async Task<ActionResult<List<Comment>>> ListOfBlockComments()
        {
            try
            {
                return StatusCode(StatusCodes.Status302Found,
                    await this._uow.AdmRepository.ListOfBlockComments());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("ListOfBlockPosts")]
        public async Task<ActionResult<List<Post>>> ListOfBlockPosts()
        {
            try
            {
                return StatusCode(StatusCodes.Status302Found,
                    await this._uow.AdmRepository.ListOfBlockPosts());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("ListOfBlockUser")]
        public async Task<ActionResult<List<User>>> ListOfBlockUser()
        {
            try
            {
                return StatusCode(StatusCodes.Status302Found,
                    await this._uow.AdmRepository.ListOfBlockUser());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

        [HttpGet("CheckIfUserIsBlocked/{email}")]
        public async Task<ActionResult<bool>> CheckIfUserIsBlocked(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest("Email is required");

                User user = this._c.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return NotFound("User not found");

                bool check = await this._uow.AdmRepository.IsBlockedUser(user);

                if (check == true)
                    return Ok(true);

                return Ok(false);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e} ");
            }
        }

    }
}
