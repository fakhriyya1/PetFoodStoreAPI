using System.ComponentModel.DataAnnotations;

namespace PetFoodStore.Models
{
    public class Animal : Base
    {
        public string Name { get; set; }

        // Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
