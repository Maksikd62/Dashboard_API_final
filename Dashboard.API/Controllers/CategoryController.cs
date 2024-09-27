using Dashboard.BLL.Services.CategoryService;
using Dashboard.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return await GetResultAsync(response);
        }
        [HttpGet("category")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return await GetResultAsync(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Категорія повинна містити назву");
            }

            model.Id = Guid.NewGuid();

            var response = await _categoryService.CreateAsync(model);
            return await GetResultAsync(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Category model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Категорія повинна містити назву");
            }

            var response = await _categoryService.UpdateAsync(model);
            return await GetResultAsync(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return await GetResultAsync(response);
        }
    }
}
