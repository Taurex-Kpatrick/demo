
using api.Services;
using api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace api.Extensions;

public static class IdentityServiceExtensions
{
public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
{
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
var tokenKey=configuration["TokenKey"] ?? throw new Exception("Token Key not found");
options.TokenValidationParameters=new TokenValidationParameters
{
  ValidateIssuerSigningKey=true,
  IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
  ValidateIssuer=false,
  ValidateAudience=false
};

}


);


    
    return services;
}

}