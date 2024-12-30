using System.Net.NetworkInformation;
using System.IdentityModel.Tokens.Jwt;
using api.Entities;
using api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Data;
namespace api.Services;



public class TokenService (IConfiguration config) : ITokenService

{

public string CreateToken (AppUser user)
{
    var tokenKey=config["TokenKey"] ?? throw new Exception("Cannot Access Token Key from appsettings");
    if (tokenKey.Length < 64) {throw new Exception("Token key too short");}

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier,user.UserName)


    };
    
    var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);


var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    Expires=DateTime.UtcNow.AddDays(7),
    SigningCredentials = creds
};

var tokenHandler = new JwtSecurityTokenHandler();
var token= tokenHandler.CreateToken(tokenDescriptor);

return tokenHandler.WriteToken(token);



}

}