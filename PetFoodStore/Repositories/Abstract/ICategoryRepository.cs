using PetFoodStore.DTOs;
using PetFoodStore.Enums;
using PetFoodStore.Models;

namespace PetFoodStore.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(CategoryType? categoryType);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(string name, CategoryType categoryType);
        Task<Category?> UpdateCategoryAsync(int id, string name, CategoryType categoryType);
        Task<Category?> SafeDeleteCategoryAsync(int id);
        Task<bool> CategoryExists(int id, CategoryType categoryType);
    }
}
