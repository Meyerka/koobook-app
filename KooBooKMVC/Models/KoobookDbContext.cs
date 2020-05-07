using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class KoobookDbContext: DbContext
    {
        public KoobookDbContext(DbContextOptions<KoobookDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                        .HasIndex(u => u.Name)
                        .IsUnique();
        }

        public DbSet<Recipe> Recipes{ get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeComponent> RecipeComponents { get; set; }
    }
}
