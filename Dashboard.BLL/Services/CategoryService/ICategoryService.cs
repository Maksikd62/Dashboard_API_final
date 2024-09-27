using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(Category model);
        Task<ServiceResponse> DeleteAsync(Guid? id);
        Task<ServiceResponse> GetAllCategoriesAsync();
        Task<ServiceResponse> GetByIdAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(Category model);
    }
}
