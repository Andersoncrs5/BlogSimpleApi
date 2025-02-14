using BlogSimpleApi.Models;
using BlogSimpleApi.SetUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"User is required");

                Category category = await this._uow.CategoryRepository.GetAsync(id);

                if (category is null)
                    return NotFound($"Category not found");

                return StatusCode(StatusCodes.Status302Found, category);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest($"User is required");

                Category category = await this._uow.CategoryRepository.GetAsync(id);

                if (category is null)
                    return NotFound($"Category not found");

                await this._uow.CategoryRepository.DeleteAsync(category);
                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status200OK, $"Category deleted");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            try
            {
                return StatusCode(StatusCodes.Status302Found, await this._uow.CategoryRepository.GetAllAsync());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Category>> Create([FromBody] Category category, long IdUser )
        {
            try
            {
                if (IdUser <= 0)
                    return BadRequest("Id is required");

                User user = await this._uow.UserRepository.GetAsync(IdUser);

                if (user == null)
                    return NotFound("User not found");

                if (user.IsAdm == false)
                    return StatusCode(StatusCodes.Status203NonAuthoritative, "You are not authorized to create a category!");

                Category categoryCreated = await this._uow.CategoryRepository.CreateAsync(category);

                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status201Created, categoryCreated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update([FromBody] Category category, long IdUser)
        {
            try
            {
                if (IdUser <= 0)
                    return BadRequest("Id is required");

                User user = await this._uow.UserRepository.GetAsync(IdUser);

                if (user == null)
                    return NotFound("User not found");

                if (user.IsAdm == false)
                    return StatusCode(StatusCodes.Status203NonAuthoritative, "You are not authorized to update a category!");

                Category CategoryUpdated = await this._uow.CategoryRepository.UpdateAsync(category);

                await this._uow.CommitAsync();
                return StatusCode(StatusCodes.Status200OK, CategoryUpdated);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error : {e}");
            }
        }

    }
}
