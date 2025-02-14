using BlogSimpleApi.DTOs;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<User>> Get(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(id);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, $"User not found");


                return StatusCode(StatusCodes.Status302Found, user);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("{id:long}/GetAllComments")]
        public async Task<ActionResult<List<Comment>>> GetAllComments(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"Id is required");

                List<Comment> comments = await this._uow.CommentRepository.GetAllUserAsync(id);

                return StatusCode(StatusCodes.Status302Found, comments);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("{id:long}/GetAllPostsOfUser")]
        public async Task<ActionResult<List<Post>>> GetAllPostsOfUser(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest($"Id is required");

                List<Post> list = await this._uow.PostRepository.GetAllUserAsync(id);

                return StatusCode(StatusCodes.Status302Found, list);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<User>> Delete(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(id);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, user);


                await this._uow.CategoryRepository.DeleteAllUserAsync(id);
                await this._uow.LikeRepository.DeleteAllUserAsync(id);
                await this._uow.FavoritePostRepository.DeleteAllUserAsync(id);
                await this._uow.CommentRepository.DeleteAllUserAsync(id);
                await this._uow.PostRepository.DeleteAllAsync(id);
                await this._uow.UserRepository.DeleteAsync(user);
                await this._uow.CommitAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            try
            {
                if (user is null)
                    return BadRequest($"User is required");
                
                User userCreated = await this._uow.UserRepository.CreateAsync(user);

                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, userCreated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<User>> Update([FromBody] User user, long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Id is required");

                if (user is null)
                    return BadRequest($"User is required");

                if (id != user.Id)
                    return BadRequest("Ids differents");

                User userUpdated = await this._uow.UserRepository.UpdateAsync(user);

                if (userUpdated is null)
                    return StatusCode(StatusCodes.Status304NotModified, $"Error the to update user");

                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, userUpdated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login([FromBody] LoginUserDTO dto)
        {
            try
            {
                if (dto is null)
                    return BadRequest($"User is required");

                return Ok(await this._uow.UserRepository.LoginAsync(dto));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest($"Email is required");

                return StatusCode(StatusCodes.Status302Found, await this._uow.UserRepository.ValidEmailAsync(email));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }


        [HttpGet("{id:long}/GetAllLikes")]
        public async Task<ActionResult<List<Like>>> GetAllLikes(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(id);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, user);

                List<Like> likeList = await this._uow.LikeRepository.GetAllUserAsync(id);

                return StatusCode(StatusCodes.Status302Found, likeList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("{id:long}/GetAllFavoritePosts")]
        public async Task<ActionResult<List<FavoritePost>>> GetAllFavoritePosts(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(id);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, user);

                List<FavoritePost> list = await this._uow.FavoritePostRepository.GetAllUserAsync(id);

                return StatusCode(StatusCodes.Status302Found, list);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("{idUser:long}/AlterStatusAdmOf/{OtherIdUser:long}")]
        public async Task<ActionResult<User>> AlterStatusAdm(long id, long OtherIdUser)
        {
            try
            {
                if (id == 0 || OtherIdUser == 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(id);
                User userOther = await this._uow.UserRepository.GetAsync(OtherIdUser);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, $"User not found with the id {id} ");
                
                if (userOther is null)
                    return StatusCode(StatusCodes.Status404NotFound, $"User not found with the id {OtherIdUser} ");

                if (user.IsAdm == false)
                    return StatusCode(StatusCodes
                        .Status203NonAuthoritative, "You are not authorized to change a status of a user!");

                User userChanged = await this._uow.UserRepository.ChangeStatusAdm(userOther);

                return StatusCode(StatusCodes.Status200OK, userChanged);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet("{idUser:long}/GetAllUserAsync")]
        public async Task<ActionResult<List<Post>>> GetAllUserAsync(long idUser)
        {
            try
            {
                if (idUser <= 0)
                    return BadRequest($"Id is required");

                User user = await this._uow.UserRepository.GetAsync(idUser);

                if (user is null)
                    return StatusCode(StatusCodes.Status404NotFound, user);

                List<Post> list = await this._uow.PostRepository.GetAllUserAsync(idUser);

                return StatusCode(StatusCodes.Status302Found, list);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

    }
}
