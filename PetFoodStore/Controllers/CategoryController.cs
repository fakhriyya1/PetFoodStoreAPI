using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFoodStore.DTOs.Categories;
using PetFoodStore.Enums;
using PetFoodStore.Models;
using PetFoodStore.Repositories.Abstract;

namespace PetFoodStore.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CategoryType? categoryType)
        {
            var categories = await categoryRepo.GetAllCategoriesAsync(categoryType);

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await categoryRepo.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound("Category not found!");

            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse(categoryDto.CategoryType, out CategoryType categoryType))
                return BadRequest("Invalid category type!");


            var category = await categoryRepo.CreateCategoryAsync(categoryDto.Name, categoryType);

            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse(categoryDto.CategoryType, out CategoryType categoryType))
                return BadRequest("Invalid category type!");

            var category = await categoryRepo.UpdateCategoryAsync(id, categoryDto.Name, categoryType);

            if (category == null)
                return NotFound("Category not found!");

            return Ok(category);
        }

        [HttpPut("safeDelete/{id:int}")]
        public async Task<IActionResult> SafeDelete([FromRoute] int id)
        {
            var category = await categoryRepo.SafeDeleteCategoryAsync(id);

            if (category == null)
                return NotFound("Category not found!");

            return Ok(category);
        }

    }
}
