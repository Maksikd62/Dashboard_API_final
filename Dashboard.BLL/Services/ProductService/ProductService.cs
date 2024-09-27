using AutoMapper;
using Dashboard.DAL.Models;
using Dashboard.DAL.Repositories.ProductRepository;
using Dashboard.DAL.ViewModels;
using Dashboard.BLL.Services.ImageService;


namespace Dashboard.BLL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;

        public ProductService(IProductRepository productRepository, IImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
        }
        public async Task<ServiceResponse> AddImageFromProductAsync(ProductImageVM model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductId);

            if (product == null)
            {
                return ServiceResponse.GetBadRequestResponse($"Продукт з id {model.ProductId} не знайдено");
            }

            var response = await _imageService.SaveImageProductAsync(model.Image);

            if (!response.Success)
            {
                return ServiceResponse.GetBadRequestResponse("Не вдалося зберегти зображення");
            }

            product.Image = response.Payload.ToString();
            var result = await _productRepository.UpdateAsync(product);

            if (!response.Success)
            {
                return ServiceResponse.GetInternalServerErrorResponse(result.Errors.First().Description);
            }

            return ServiceResponse.GetOkResponse("Зображення успішно додано");
        }
        public async Task<ServiceResponse> CreateAsync(Product model)
        {
            var result = await _productRepository.CreateAsync(model);

            if (result.Succeeded)
            {
                return ServiceResponse.GetOkResponse($"Продукт {model.Name} успішно створений");
            }

            return ServiceResponse.GetBadRequestResponse(message: "Не вдалося створити продукт", errors: result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<ServiceResponse> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося видалити продукт", errors: $"Продукт {id} не знайдено");
            }

            var result = await _productRepository.DeleteAsync(id);

            if (!result.Succeeded)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося видалити продукт", errors: result.Errors.Select(e => e.Description).ToArray());
            }

            return ServiceResponse.GetOkResponse("Продукт успішно видалений");
        }

        public async Task<ServiceResponse> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return ServiceResponse.GetOkResponse("Список продуктів", products);
        }

        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося отримати продукт", errors: $"Продукт {id} не знайдено");
            }

            return ServiceResponse.GetOkResponse(message: "Продукт отримано", payload: product);
        }

        public async Task<ServiceResponse> UpdateAsync(Product model)
        {
            var product = await _productRepository.GetByIdAsync(model.Id);

            if (product == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Помилка оновлення", errors: $"Продукт з id {model.Id} не знайдено");
            }

            var updateResult = await _productRepository.UpdateAsync(model);

            if (updateResult.Succeeded)
            {
                return ServiceResponse.GetOkResponse("Продукт успішно оновлено");
            }
            else
            {
                return ServiceResponse.GetBadRequestResponse(message: "Помилка оновлення", errors: updateResult.Errors.Select(e => e.Description).ToArray());
            }
        }
    }
}
