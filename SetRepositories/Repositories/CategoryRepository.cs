using BlogSimpleApi.Datas;
using BlogSimpleApi.Models;
using BlogSimpleApi.SetRepositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSimpleApi.SetRepositories.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                if (category is null)
                    throw new ArgumentNullException(nameof(category), "Category is required");

                var result = await _context.Categories.AddAsync(category);

                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task DeleteAllUserAsync(long id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id is required");

                List<Category> Categories = await this._context
                    .Categories.AsNoTracking().Where(c => c.UserId == id).ToListAsync();
                this._context.Categories.RemoveRange(Categories);
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category), "Category is required");

                _context.Categories.Remove(category);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<Category> GetAsync(long id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid category ID", nameof(id));

                Category category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (category is null)
                    throw new Exception("Category not found");

                return category;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category), "Category is required");

                _context.Entry(category).State = EntityState.Modified;

                return category;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);
            }
        }
    }
}
