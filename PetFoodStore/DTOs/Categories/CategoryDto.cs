using PetFoodStore.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Categories
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryType { get; set; }
    }
}
