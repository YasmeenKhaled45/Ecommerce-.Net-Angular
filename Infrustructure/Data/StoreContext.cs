using System;
using Core.Entities;
using Infrustructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products{get;set;}
    public DbSet<ShoppingCart> ShoppingCarts{get;set;}
     public DbSet<Address> Addresses{get;set;}
    public DbSet<Items> Items{get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Product>()
        .Property(p => p.Rate)
        .HasColumnType("decimal(18,2)"); 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);  
    }
}
