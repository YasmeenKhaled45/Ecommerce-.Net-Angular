using System;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Extensions;

public static class UserExtensions
{
  public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager , ClaimsPrincipal claims)
  {
     var user = await userManager.Users
    .FirstOrDefaultAsync(x => x.Email == claims.GetEmail());
     if(user == null) throw new AuthenticationException("user not found");
     return user;

  }  

  public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager , ClaimsPrincipal claims)
  {
     var user = await userManager.Users.Include(x=>x.Address)
    .FirstOrDefaultAsync(x => x.Email == claims.GetEmail());
     if(user == null) throw new AuthenticationException("user not found");
     return user;


  }  
  
  public static string GetEmail(this ClaimsPrincipal claims)
  {
     var email = claims.FindFirstValue(ClaimTypes.Email) ?? 
         throw new AuthenticationException("Email claim not found!");

     return email;
  }
}
