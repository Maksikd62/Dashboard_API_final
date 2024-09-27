using Dashboard.DAL.Models;
using Dashboard.DAL.Repositories.CategoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> CreateAsync(Category model)
        {
            var result = await _categoryRepository.CreateAsync(model);

            if (result.Succeeded)
            {
                return ServiceResponse.GetOkResponse($"Катерію {model.Name} успішно створено");
            }

            return ServiceResponse.GetBadRequestResponse(message: "Не вдалося створити категорію", errors: result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<ServiceResponse> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося видалити категорію", errors: $"Катерія {id} не знайдено");
            }

            var result = await _categoryRepository.DeleteAsync(id);

            if (!result.Succeeded)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося видалити категорію", errors: result.Errors.Select(e => e.Description).ToArray());
            }

            return ServiceResponse.GetOkResponse("Катерію успішно видалений");
        }

        public async Task<ServiceResponse> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return ServiceResponse.GetOkResponse("Список категорій", categories);
        }

        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var product = await _categoryRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Не вдалося отримати категорію", errors: $"Катерія {id} не знайдено");
            }

            return ServiceResponse.GetOkResponse(message: "Катерію отримано", payload: product);
        }

        public async Task<ServiceResponse> UpdateAsync(Category model)
        {
            var category = await _categoryRepository.GetByIdAsync(model.Id);

            if (category == null)
            {
                return ServiceResponse.GetBadRequestResponse(message: "Помилка оновлення", errors: $"Катерія з id {model.Id} не знайдено");
            }

            var updateResult = await _categoryRepository.UpdateAsync(model);

            if (updateResult.Succeeded)
            {
                return ServiceResponse.GetOkResponse("Катерію успішно оновлено");
            }
            else
            {
                return ServiceResponse.GetBadRequestResponse(message: "Помилка оновлення", errors: updateResult.Errors.Select(e => e.Description).ToArray());
            }
        }
    }
}
