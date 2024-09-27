using Dashboard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<IdentityResult> CreateAsync(Category product);
        Task<IdentityResult> UpdateAsync(Category newModel);
        Task<IdentityResult> DeleteAsync(Guid? id);
    }
}
