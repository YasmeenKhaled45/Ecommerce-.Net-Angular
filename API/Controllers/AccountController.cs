using System.Security.Claims;
using Core.DTOS;
using Core.Entities;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        
        public AccountController( SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        [Authorize]
        [HttpGet("getsecret")]
        public  IActionResult GetSecret()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok("Hi" + name + "with" + id );
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var user = new AppUser{
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email
            };
          var res = await signInManager.UserManager.CreateAsync(user,dto.Password);
          if(!res.Succeeded){
            foreach(var error in res.Errors){
                ModelState.AddModelError(error.Code,error.Description);
            }
             return ValidationProblem();
          }
           return Ok();
        }
[Authorize]
[HttpGet("user-info")]
public async Task<ActionResult> GetUserInfo()
{

        if (User.Identity?.IsAuthenticated == false) return NoContent();
        var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
        return Ok(new 
        {
            user.FirstName,
            user.LastName,
            user.Email,
            Address = user.Address?.toDTo()
        });
}


        [HttpPost("address")]
        public async Task<ActionResult> AddOrUpdateAddress(AddressDTO addressDTO)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if(user.Address == null)
            {
                user.Address = addressDTO.TOEntity();
            }
            else{
                user.Address.UpdateAddress(addressDTO);
            }
            var result = await signInManager.UserManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest("Problem updating the address");

            return Ok(user.Address.toDTo());
        }
    }
}
