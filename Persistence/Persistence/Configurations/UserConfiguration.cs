﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.HasKey(u=>u.Id);

           builder.Property(u=>u.Name)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(u => u.Password)
                   .IsRequired();
        }
    }
}
