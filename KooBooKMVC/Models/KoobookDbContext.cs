using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class KoobookDbContext: IdentityDbContext
    {
        public KoobookDbContext(DbContextOptions<KoobookDbContext> options)
        : base(options)
        {

        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ingredient>()
                        .HasIndex(u => u.Name)
                        .IsUnique();
        }

        public DbSet<Recipe> Recipes{ get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeComponent> RecipeComponents { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
