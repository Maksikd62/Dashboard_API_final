using Dashboard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<IdentityResult> CreateAsync(Product product);
        Task<IdentityResult> UpdateAsync(Product newModel);
        Task<IdentityResult> DeleteAsync(Guid? id);
    }
}
