using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using api.Entities;
using System.Text;
using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
using api.DTOs;
using api.Interfaces;

namespace api.controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseAPIController
{

[HttpPost("register")]
public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
{

    if (await UserExists(registerDTO.Username))
    {
        return BadRequest("Username is taken");
    }
    using var hmac = new  HMACSHA512();
    var user = new AppUser {
        UserName=registerDTO.Username.ToLower(), 
        PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
        PasswordSalt=hmac.Key
    };
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return new UserDTO
    {
        UserName=user.UserName,
        Token=tokenService.CreateToken(user)
    };

}
[HttpPost("login")]
public async Task<ActionResult<UserDTO>> Login (LoginDto loginData)
{
var user = await context.Users.FirstOrDefaultAsync(x =>
 x.UserName==loginData.Username.ToLower());

if (user==null) return Unauthorized("Invalid Username");

using var hmac=new HMACSHA512(user.PasswordSalt);
var ComputeHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginData.Password));

for(int i=0;i<ComputeHash.Length;i++)
{
if (ComputeHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password");


}

return new UserDTO
{
    UserName=user.UserName,
    Token=tokenService.CreateToken(user)
};

}


[HttpPost("login")]
private async Task<Boolean> UserExists (string username)
{
    return await context.Users.AnyAsync(x => x.UserName.ToLower()==username.ToLower());
}

}





 