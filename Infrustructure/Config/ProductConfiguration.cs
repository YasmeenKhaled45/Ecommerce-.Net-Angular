using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrustructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Name).IsUnique(); 
        builder.Property(x=>x.Description).HasMaxLength(1000);
        builder.Property(x=>x.QuantityInStock).HasDefaultValue(1);
    }
}

