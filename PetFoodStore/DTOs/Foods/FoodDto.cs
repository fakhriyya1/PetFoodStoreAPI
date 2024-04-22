using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Foods
{
    public class FoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Relations
        public int AnimalId { get; set; }
        public int? CategoryId { get; set; }
    }
}
