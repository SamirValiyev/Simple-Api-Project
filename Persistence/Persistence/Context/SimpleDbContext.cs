using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class SimpleDbContext:DbContext
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories => this.Set<Category>();  
        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<Role> Roles => this.Set<Role>();   
        public DbSet<User> Users => this.Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category(){Id=1,Definition="Electronics"},
                new Category(){Id=2,Definition="Clothing"},
                new Category(){Id=3,Definition="Books"}

            });
            modelBuilder.Entity<Product>().HasData(new Product[] 
            {
                new Product(){Id=1,Name="Acer Swift 3",Price=5000,Stock=100,CategoryId=1},
                new Product(){Id=2,Name="Polo T-Shirt",Price=50,Stock=1000,CategoryId=2},
                new Product(){Id=3,Name="Atomic Habits",Price=100,Stock=1000,CategoryId=3}

            });

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
