using Dashboard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = _context.Categories.AsNoTracking();

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<IdentityResult> CreateAsync(Category category)
        {
            if (category == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Категорію не знайдено" });
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> UpdateAsync(Category newModel)
        {
            if (newModel == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Категорію не знайдено" });
            }
            var model = await _context.Categories.FirstOrDefaultAsync(p => p.Id == newModel.Id);
            if (model != null)
            {
                _context.Categories.Remove(model);
            }
            await _context.Categories.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Категорію не знайдено" });
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = "Помилка" });
        }
    }
}
