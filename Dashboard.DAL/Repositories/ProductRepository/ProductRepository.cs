using Dashboard.DAL.Models;
using Dashboard.DAL.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dashboard.DAL.Models.Identity;

namespace Dashboard.DAL.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() 
        {
            var products = _context.Products
                .Include(p => p.Category);

            return await products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<IdentityResult> CreateAsync(Product product)
        {
            if (product == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Продукт не знайдено" });
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> UpdateAsync(Product newModel)
        {
            if (newModel == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Продукт не знайдено" });
            }
            var model = await _context.Products.FirstOrDefaultAsync(p => p.Id == newModel.Id);
            if (model != null) 
            { 
                _context.Products.Remove(model);
            }
            await _context.Products.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(Guid? id)
        {
            if (id == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Продукт не знайдено" });
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = "Помилка" });
        }

    }
}
