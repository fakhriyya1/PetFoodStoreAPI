using PetFoodStore.DTOs.Foods;
using PetFoodStore.Models;

namespace PetFoodStore.Repositories.Abstract
{
    public interface IFoodRepository
    {
        Task<List<FoodDto>> GetAllFoodsAsync(int? categoryId, int? animalId);
        Task<Food?> GetFoodByIdAsync(int id);
        Task<Food> CreateFoodAsync(CreateFoodDto createFoodDto);
        Task<Food> UpdateFoodAsync(int id, UpdateFoodDto updateFoodDto);
    }
}
