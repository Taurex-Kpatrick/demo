using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.controllers;
 
 
public class UsersController (DataContext context): BaseAPIController
{
[AllowAnonymous]
[HttpGet]
public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
{
var users = await context.Users.ToListAsync();
return Ok(users);

}
[Authorize]
[HttpGet("{id}")]
public async Task<ActionResult<AppUser>> GetUser(int Id)
{
var user = await context.Users.FindAsync(Id);
if (user==null)
{
    return NotFound();
}

 

return Ok(user);

}


}