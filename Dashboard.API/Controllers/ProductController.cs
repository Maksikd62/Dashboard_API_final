using Dashboard.BLL.Services.ProductService;
using Dashboard.DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Dashboard.DAL.Models;


namespace Dashboard.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("ImageFromProduct")]
        public async Task<IActionResult> AddImageFromProductAsync([FromForm] ProductImageVM model)
        {
            if (model.Image == null)
            {
                return BadRequest("Зображення не знайдено");
            }

            if (model.ProductId == null)
            {
                return BadRequest("Невірно вказано id користувача");
            }

            var response = await _productService.AddImageFromProductAsync(model);

            return await GetResultAsync(response);
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllProductsAsync();
            return await GetResultAsync(response);
        }
        [HttpGet("product")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _productService.GetByIdAsync(id);
            return await GetResultAsync(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Продукт повинен містити назву");
            }

            model.Id = Guid.NewGuid();

            var response = await _productService.CreateAsync(model);
            return await GetResultAsync(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Product model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Продукт повинен містити назву");
            }

            var response = await _productService.UpdateAsync(model);
            return await GetResultAsync(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _productService.DeleteAsync(id);
            return await GetResultAsync(response);
        }
    }
}
