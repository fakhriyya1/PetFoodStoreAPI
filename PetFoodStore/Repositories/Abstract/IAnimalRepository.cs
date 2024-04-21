using PetFoodStore.DTOs.Animals;
using PetFoodStore.Models;

namespace PetFoodStore.Repositories.Abstract
{
    public interface IAnimalRepository
    {
        Task<List<AnimalDto>> GetAllAnimalsAsync(int? categoryId);
        Task<Animal?> GetAnimalByIdAsync(int id); 
        Task<Animal> CreateAnimalAsync(AnimalCreateDto animalDto);
        Task<Animal?> UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdateDto);
        Task<Animal?> SafeDeleteCategoryAsync(int id);
    }
}
