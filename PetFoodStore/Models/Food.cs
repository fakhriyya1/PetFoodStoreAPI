﻿namespace PetFoodStore.Models
{
    public class Food : Base
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Relations

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
