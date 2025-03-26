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
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
