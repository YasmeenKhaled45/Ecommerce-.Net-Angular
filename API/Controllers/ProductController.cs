using API.RequestHelpers;
using AutoMapper;
using Core.DTOS;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrustructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericRepository<Product> repo ) : ControllerBase
    {
        private readonly IGenericRepository<Product> repository = repo;
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductFilterSpecification(specParams);
            var products = await repository.ListAllAsync(spec);
            var count = await repository.CountAsync(spec);
            var pagination = new Pagination<Product>(specParams.PageIndex,specParams.PageSize,count,products);
            return Ok(pagination);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            var product = await repository.GetById(id);
            return product is null ? NotFound() : Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpec();
              return Ok(await repository.ListAllAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpec();
             return Ok(await repository.ListAllAsync(spec));
        }
       
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repository.Add(product);
            if(await repository.SaveChangesAsync())
            {
                 return Created();
            }

           return BadRequest("Problem creating the product!");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id , Product product)
        {
            if(product.Id != id || !ProductExists(id))
               return BadRequest("Cant update this product!");

           repository.Update(product);
            if(await repository.SaveChangesAsync()){
                return NoContent();
            }
            return BadRequest("Problem updating the product!");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetById(id);
            if(product is null) return NotFound();

            repository.Remove(product);
            if(await repository.SaveChangesAsync()){
                return NoContent();
            }
            return BadRequest("Problem deleting the product!");
        }
        private bool ProductExists(int id){
         return repository.Exists(id);
        }
    }
}
