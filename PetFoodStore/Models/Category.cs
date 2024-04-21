using PetFoodStore.Enums;

namespace PetFoodStore.Models
{
    public class Category:Base
    {
        public string Name { get; set; }
        public CategoryType CategoryType { get; set; }

        // Relations
        public ICollection<Food> Foods { get; set; } = [];
        public ICollection<Animal> Animals { get; set; } = [];
    }
}
