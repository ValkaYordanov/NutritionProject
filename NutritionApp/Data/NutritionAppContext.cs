using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NutritionApp.Data
{
    public class NutritionAppContext : IdentityDbContext<AppUser>
    {
        public NutritionAppContext()
        {
        }

        

        public NutritionAppContext(DbContextOptions<NutritionAppContext> options) : base(options) { }
       // public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Intake> Intakes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<ProductNutrition> ProductNutritions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Intake>().ToTable("Intake");
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Product>().ToTable("Product");
            //modelBuilder.Entity<ProductNutrition>().ToTable("ProductNutrition");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Gender>().ToTable("Gender");

            modelBuilder.Entity<Intake>().Property(b => b.Day).HasColumnType("datetime2");
            modelBuilder.Entity<Intake>().Property(b => b.Quantity).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Ingredient>().Property(b => b.Quantity).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Meal>().Property(b => b.Quantity).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Meal>().Property(b => b.MealName).HasColumnType("varchar(300)");
            modelBuilder.Entity<Product>().Property(b => b.Brand).HasColumnType("varchar(60)");
            modelBuilder.Entity<Product>().Property(b => b.ProductName).HasColumnType("varchar(100)");
            modelBuilder.Entity<Tag>().Property(b => b.TagName).HasColumnType("varchar(50)");
            modelBuilder.Entity<Gender>().Property(b => b.Label).HasColumnType("varchar(50)");
            //modelBuilder.Entity<ProductNutrition>().Property(b => b.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.Fat).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.FromFatSaturated).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.Carbs).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.FromCarbsSuggar).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.Protein).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(b => b.Salt).HasColumnType("decimal(18,2)");



        }
    }
}
