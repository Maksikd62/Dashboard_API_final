using Dashboard.DAL.Models;
using Dashboard.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse> AddImageFromProductAsync(ProductImageVM model);
        Task<ServiceResponse> CreateAsync(Product model);
        Task<ServiceResponse> DeleteAsync(Guid? id);
        Task<ServiceResponse> GetAllProductsAsync();
        Task<ServiceResponse> GetByIdAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(Product model);
    }
}
