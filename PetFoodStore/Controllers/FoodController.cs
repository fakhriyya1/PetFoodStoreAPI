using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFoodStore.DTOs.Animals;
using PetFoodStore.DTOs.Foods;
using PetFoodStore.Enums;
using PetFoodStore.Models;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository foodRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IAnimalRepository animalRepo;

        public FoodController(IFoodRepository foodRepo, ICategoryRepository categoryRepo, IAnimalRepository animalRepo)
        {
            this.foodRepo = foodRepo;
            this.categoryRepo = categoryRepo;
            this.animalRepo = animalRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, int? animalId)
        {
            var foods = await foodRepo.GetAllFoodsAsync(categoryId, animalId);

            if (categoryId != null && !await categoryRepo.CategoryExists((int)categoryId, CategoryType.Food))
                return BadRequest("Category doesn't exist");
            
            if (animalId != null && !await animalRepo.AnimalExists((int)animalId))
                return BadRequest("Animal doesn't exist");

            return Ok(foods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var food = await foodRepo.GetFoodByIdAsync(id);

            if (food == null)
                return NotFound("Food not found!");

            return Ok(food);
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateFoodDto createFoodDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createFoodDto.CategoryId!=null && !await animalRepo.AnimalExists(createFoodDto.AnimalId))
                return BadRequest("Animal doesn't exist");

            if (createFoodDto.CategoryId != null && !await categoryRepo.CategoryExists((int)createFoodDto.CategoryId, CategoryType.Food))
                return BadRequest("Category doesn't exist");

            var food = await foodRepo.CreateFoodAsync(createFoodDto);

            return CreatedAtAction("GetById", new { id = food.Id }, food);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateFoodDto updateFoodDto)
        {
            if (updateFoodDto.AnimalId == null)
                ModelState.AddModelError("AnimalId", "Animal cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateFoodDto.CategoryId != null! && await animalRepo.AnimalExists((int)updateFoodDto.AnimalId))
                return BadRequest("Animal doesn't exist");

            if (updateFoodDto.CategoryId != null && !await categoryRepo.CategoryExists((int)updateFoodDto.CategoryId, CategoryType.Food))
                return BadRequest("Category doesn't exist");

            var food=await foodRepo.UpdateFoodAsync(id, updateFoodDto);

            if (food == null)
                return BadRequest("Food doesn't exist!");

            return Ok(food);
        }

    }
}
