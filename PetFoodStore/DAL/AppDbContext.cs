using Microsoft.EntityFrameworkCore;
using PetFoodStore.Models;

namespace PetFoodStore.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Food>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
