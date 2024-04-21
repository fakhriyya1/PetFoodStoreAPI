using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.DTOs.Animals
{
    public class AnimalUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
