using PetFoodStore.Models;
using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Foods
{
    public class CreateFoodDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        // Relations

        [Required]
        public int AnimalId { get; set; }
        public int? CategoryId { get; set; } = null;
    }
}
