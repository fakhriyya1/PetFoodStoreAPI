using Microsoft.EntityFrameworkCore;
using PetFoodStore.DAL;
using PetFoodStore.DTOs;
using PetFoodStore.Enums;
using PetFoodStore.Models;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CategoryType? categoryType)
        {
            IQueryable<Category> query = context.Categories.Where(c => c.IsActive);

            if (categoryType.HasValue)
            {
                query = query.Where(c => c.CategoryType == categoryType);
            }

            var categories = await query.ToListAsync();

            return categories;
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

            if (category == null)
                return null;

            return category;
        }

        public async Task<Category> CreateCategoryAsync(string name, CategoryType categoryType)
        {
            var category = new Category
            {
                Name = name,
                CategoryType = categoryType
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, string name, CategoryType categoryType)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            category.Name = name;
            category.CategoryType = categoryType;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> SafeDeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            category.IsActive = false;
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> CategoryExists(int id, CategoryType categoryType)
        {
            return await context.Categories.AnyAsync(c => c.Id == id && c.CategoryType == categoryType && c.IsActive);
        }
    }
}
