using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[ApiController]
[Route("api/[controller]")]
public class UsersController (DataContext context): ControllerBase
{
[HttpGet]
public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
{
var users = await context.Users.ToListAsync();
return Ok(users);

}
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