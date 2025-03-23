using System;
using Core.Entities;
using Core.Interfaces;
using Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Services;

public class CartService : ICartService
{
    private readonly StoreContext _context;

    public CartService(StoreContext context)
    {
        _context = context;
    }

public async Task<ShoppingCart?> GetCartAsync(int id)
{
    if (id == 0)
    {
        return new ShoppingCart
        {
            Id = 0,
            items = new List<Items>() // Map to `Items` here
        };
    }

    return await _context.ShoppingCarts
        .Include(c => c.items)
        .Select(c => new ShoppingCart
        {
            Id = c.Id,
            items = c.items.Select(ci => new Items
            {
                ProductId = ci.ProductId,
                ProductName = ci.ProductName,
                Price = ci.Price,
                Quantity = ci.Quantity,
                PictureUrl = ci.PictureUrl,
                Brand = ci.Brand,
                Type = ci.Type
            }).ToList() // Map CartItems to Items
        })
        .FirstOrDefaultAsync(c => c.Id == id);
}


public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
{
    if (cart == null || !cart.items.Any())
    {
        return null;
    }

    var existingCart = await _context.ShoppingCarts
        .Include(c => c.items)
        .FirstOrDefaultAsync(c => c.Id == cart.Id);

    if (existingCart != null)
    {
        // Update existing cart items
        foreach (var item in cart.items)
        {
            var existingItem = existingCart.items
                .FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                existingItem = new Items
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl,
                    Brand = item.Brand,
                    Type = item.Type
                };

                existingCart.items.Add(existingItem);
            }
        }

        await _context.SaveChangesAsync();
        return await _context.ShoppingCarts
            .Include(c => c.items)
            .FirstOrDefaultAsync(c => c.Id == existingCart.Id);
    }
    else
    {
        // Do not set cart.Id manually; let the DB generate it
        cart.Id = 0; // Ensure Id is set to 0 so that the database will generate it

        _context.ShoppingCarts.Add(cart);
        await _context.SaveChangesAsync();

        return await _context.ShoppingCarts
            .Include(c => c.items)
            .FirstOrDefaultAsync(c => c.Id == cart.Id); // cart.Id will be set by the DB
    }
}



 public async Task<bool> DeleteCartAsync(int id)
{
    var cart = await _context.ShoppingCarts.Include(c => c.items).FirstOrDefaultAsync(c => c.Id == id);
    if (cart == null) return false;

         _context.ShoppingCarts.Remove(cart);
     _context.Items.RemoveRange(cart.items);
    // Then remove the cart itself
    
    await _context.SaveChangesAsync();

    return true;
}

}
