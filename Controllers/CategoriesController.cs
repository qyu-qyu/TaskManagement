using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Any authenticated user can view categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // Only Admin can create categories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.CreateAsync(dto);
            return Ok(category);
        }

        // Only Admin can delete categories
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Category not found." });

            return Ok(new { message = "Category deleted successfully." });
        }
    }
}