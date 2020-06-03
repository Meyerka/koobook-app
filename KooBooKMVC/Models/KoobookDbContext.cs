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


            modelBuilder.Entity<UserRecipe>()
            .HasKey(ub => new { ub.UserId, ub.RecipeId });

            modelBuilder.Entity<UserRecipe>()
                .HasOne(ub => ub.ApplicationUser)
                .WithMany(au => au.UserRecipes)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserRecipe>()
                .HasOne(ub => ub.Recipe)
                .WithMany() 
                .HasForeignKey(ub => ub.RecipeId);
        }

        public DbSet<Recipe> Recipes{ get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeComponent> RecipeComponents { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserRecipe> UserRecipe { get; set; }
    }
}
