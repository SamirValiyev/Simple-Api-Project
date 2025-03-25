using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(p=>p.Category)
                   .WithMany(c=>c.Products)
                   .HasForeignKey(p=>p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p=>p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(p=>p.Stock)
                   .IsRequired();

            builder.Property(p=>p.CategoryId)
                   .IsRequired();

            builder.Property(p=>p.Category)
                   .IsRequired();
        }
    }
}
