using Microsoft.EntityFrameworkCore;
using PetFoodStore.DAL;
using PetFoodStore.DTOs.Animals;
using PetFoodStore.Enums;
using PetFoodStore.Models;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Repositories.Concrete
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext context;

        public AnimalRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<AnimalDto>> GetAllAnimalsAsync(int? categoryId)
        {
            IQueryable<Animal> query = context.Animals.Where(a => a.IsActive);

            if (categoryId != null)
            {
                query = query.Where(a => a.CategoryId == categoryId && a.Category.IsActive);
            }

            //var animals = await query.ToListAsync();

            var animalDto = await query.Select(animal => new AnimalDto
            {
                Id = animal.Id,
                CategoryId = animal.CategoryId,
                Name = animal.Name
            }).ToListAsync();

            return animalDto;
        }

        public async Task<Animal?> GetAnimalByIdAsync(int id)
        {
            var animal = await context.Animals.FirstOrDefaultAsync(a => a.IsActive && a.Id == id);

            if (animal == null)
                return null;

            return animal;
        }

        public async Task<Animal> CreateAnimalAsync(AnimalCreateDto animalDto)
        {
            var animal = new Animal
            {
                Name = animalDto.Name,
                CategoryId = animalDto.CategoryId
            };

            await context.Animals.AddAsync(animal);
            await context.SaveChangesAsync();

            return animal;
        }

        public async Task<Animal?> UpdateAnimalAsync(int id, AnimalUpdateDto animalUpdateDto)
        {
            var animal = await GetAnimalByIdAsync(id);

            if (animal == null)
                return null;

            animal.Name = animalUpdateDto.Name;
            animal.CategoryId = (int)animalUpdateDto.CategoryId;

            context.Animals.Update(animal);
            await context.SaveChangesAsync();

            return animal;
        }

        public async Task<Animal?> SafeDeleteCategoryAsync(int id)
        {
            var animal = await GetAnimalByIdAsync(id);

            if (animal == null)
                return null;

            animal.IsActive = false;
            await context.SaveChangesAsync();

            return animal;
        }

        public async Task<bool> AnimalExists(int id)
        {
            return await context.Animals.AnyAsync(a => a.Id == id && a.IsActive && a.Category.IsActive);
        }
    }
}
