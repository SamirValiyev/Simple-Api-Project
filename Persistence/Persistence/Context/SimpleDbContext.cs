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
                new Category(){Id=1,Definition="Electronics",Name="Electronic"},
                new Category(){Id=2,Definition="Clothing",Name="Cloth"},
                new Category(){Id=3,Definition="Books",Name="Book"}

            });
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User(){Id=1,Name="Member",Password="123",RoleId=2},
                new User(){Id=2,Name="Admin",Password="1234",RoleId=1}
            });
            modelBuilder.Entity<Role>().HasData(new Role[] 
            { 
               new Role(){Id=1,Definition="Admin"},
               new Role(){Id=2,Definition="Member"}
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
