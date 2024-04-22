using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Foods
{
    public class UpdateFoodDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        // Relations
        public int? AnimalId { get; set; }
        public int? CategoryId { get; set; }
    }
}
