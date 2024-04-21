using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFoodStore.DTOs.Animals;
using PetFoodStore.Enums;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Controllers
{
    [Route("api/animal")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository animalRepo;
        private readonly ICategoryRepository categoryRepo;

        public AnimalController(IAnimalRepository animalRepo, ICategoryRepository categoryRepo)
        {
            this.animalRepo = animalRepo;
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
        {
            var animals = await animalRepo.GetAllAnimalsAsync(categoryId);

            if (categoryId != null && !await categoryRepo.CategoryExists((int)categoryId, CategoryType.Animal))
                return BadRequest("Category doesn't exist");

            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var animal = await animalRepo.GetAnimalByIdAsync(id);

            if (animal == null)
                return BadRequest("Animal doesn't exist");

            var animalDto = new AnimalDto
            {
                Id = animal.Id,
                CategoryId = animal.CategoryId,
                Name = animal.Name
            };

            return Ok(animalDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AnimalCreateDto animalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await categoryRepo.CategoryExists(animalDto.CategoryId, CategoryType.Animal))
                return BadRequest("Category doesn't exist!");

            var category = await animalRepo.CreateAnimalAsync(animalDto);

            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AnimalUpdateDto animalUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await categoryRepo.CategoryExists(animalUpdateDto.CategoryId, CategoryType.Animal))
                return BadRequest("Category doesn't exist!");

            var animal = await animalRepo.UpdateAnimalAsync(id, animalUpdateDto);

            if (animal == null)
                return BadRequest("Animal doesn't exist!");

            return Ok(animal);
        }

        [HttpPut("safeDelete/{id}")]
        public async Task<IActionResult> SafeDelete([FromRoute] int id)
        {
            var animal = await animalRepo.SafeDeleteCategoryAsync(id);

            if (animal == null)
                return BadRequest("Animal doesn't exist!");

            return Ok(animal);
        }
    }
}
