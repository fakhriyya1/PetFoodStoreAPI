using Microsoft.EntityFrameworkCore;
using PetFoodStore.DAL;
using PetFoodStore.DTOs.Animals;
using PetFoodStore.DTOs.Foods;
using PetFoodStore.Enums;
using PetFoodStore.Models;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Repositories.Concrete
{
    public class FoodRepository : IFoodRepository
    {
        private readonly AppDbContext context;

        public FoodRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Food> CreateFoodAsync(CreateFoodDto createFoodDto)
        {
            var food = new Food
            {
                Name = createFoodDto.Name,
                Price = createFoodDto.Price,
                Stock = createFoodDto.Stock,
                AnimalId = createFoodDto.AnimalId
            };

            if (createFoodDto.CategoryId != null)
            {
                food.CategoryId = createFoodDto.CategoryId;
            }

            await context.Foods.AddAsync(food);
            await context.SaveChangesAsync();

            return food;
        }

        public async Task<List<FoodDto>> GetAllFoodsAsync(int? categoryId, int? animalId)
        {
            IQueryable<Food> query = context.Foods.Where(a => a.IsActive && a.Animal.IsActive);

            if (categoryId != null && animalId != null)
            {
                query = query.Where(a => a.CategoryId == categoryId && a.Category.IsActive && a.AnimalId == animalId);
            }
            else if (categoryId != null)
            {
                query = query.Where(a => a.CategoryId == categoryId && a.Category.IsActive);
            }
            else
            {
                query = query.Where(a => a.AnimalId == animalId);
            }

            var foodDto = await query.Select(food => new FoodDto
            {
                Id = food.Id,
                CategoryId = food.CategoryId,
                AnimalId = food.AnimalId,
                Name = food.Name,
                Price = food.Price,
                Stock = food.Stock
            }).ToListAsync();

            return foodDto;
        }

        public async Task<Food?> GetFoodByIdAsync(int id)
        {
            var food = await context.Foods.FirstOrDefaultAsync(f => f.Id == id && f.IsActive && f.Category.IsActive && f.Animal.IsActive);

            if (food == null)
                return null;

            return food;
        }

        public async Task<Food> UpdateFoodAsync(int id, UpdateFoodDto updateFoodDto)
        {
            var food = await GetFoodByIdAsync(id);

            if (food == null)
                return null;

            food.Name = updateFoodDto.Name;
            food.Stock = updateFoodDto.Stock;
            food.Price = updateFoodDto.Price;
            food.CategoryId = updateFoodDto.CategoryId;
            food.AnimalId = (int)updateFoodDto.AnimalId;

            context.Update(food);
            await context.SaveChangesAsync();

            return food;
        }
    }
}
