using Microsoft.AspNetCore.Mvc;

namespace Axolotl.Api.Controllers
{
 [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly GetAllCategoriesUseCase _getAll;
        private readonly GetCategoryByIdUseCase _getById;
        private readonly CreateCategoryUseCase _create;
        private readonly UpdateCategoryUseCase _update;
        private readonly DeleteCategoryUseCase _delete;

        public CategoriesController(
            GetAllCategoriesUseCase getAll,
            GetCategoryByIdUseCase getById,
            CreateCategoryUseCase create,
            UpdateCategoryUseCase update,
            DeleteCategoryUseCase delete)
        {
            _getAll = getAll;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _getAll.ExecuteAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _getById.ExecuteAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var result = await _create.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryRequest request)
        {
            var success = await _update.ExecuteAsync(id, request);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _delete.ExecuteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}