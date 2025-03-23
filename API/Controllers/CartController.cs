using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService service) : ControllerBase
    {
       private readonly ICartService service1 = service;
      
       [HttpGet("{Id}")]
       public async Task<ActionResult<ShoppingCart>> GetCartAsync( [FromRoute] int Id)
       {
            var cart = await service1.GetCartAsync(Id);
            return cart is null ? NotFound() :  Ok(cart);
       }


       [HttpPost("setcart")]
       public async Task<ActionResult<ShoppingCart>> SetCart([FromBody]ShoppingCart cart)
       {
             var updatedCart = await service1.SetCartAsync(cart);
             return updatedCart is null ? NotFound("Cart can not be updated!") : Ok(cart);
       }

       [HttpDelete("{Id}")]
       public async Task<ActionResult> DeleteCartAsync([FromRoute] int Id)
       {
           var res = await service1.DeleteCartAsync(Id);
           if(!res) return BadRequest("Problem deleting the Cart");

           return Ok();
       }
    }
}
