using System;
using AutoMapper;
using Core.DTOS;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Data;

public class ProductRepository(StoreContext context ) : IProductRepository
{
    private readonly StoreContext Context  = context;
    public void AddProduct(Product product)
    {
        Context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        Context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await Context.Products.Select(x=>x.Brand).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductById(int Id)
    {
       return await Context.Products.FindAsync(Id);

    }

    public async Task<IReadOnlyList<Product>> GetProducts(string? type , string? brand,string? sort)
    {
        var query = Context.Products.AsQueryable();
        if(!string.IsNullOrWhiteSpace(type))
           query = query.Where(x=>x.Type == type);

        if(!string.IsNullOrWhiteSpace(brand))
          query = query.Where(x=>x.Brand == brand);
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x=>x.Price),
                "priceDesc" => query.OrderByDescending(x=>x.Price),
                "TopRated" => query.OrderByDescending(x=>x.Rate),
                _ => query.OrderBy(x=>x.Name)
            };
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await Context.Products.Select(x=>x.Type).Distinct().ToListAsync();
    }

    public bool ProductExists(int Id)
    {
        return Context.Products.Any(x=>x.Id == Id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        Context.Entry(product).State = EntityState.Modified;
    }
}
