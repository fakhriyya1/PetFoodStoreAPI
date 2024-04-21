using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Animals
{
    public class AnimalDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
