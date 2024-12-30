using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Services;
using api.Interfaces;



namespace api.Extensions;


public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {

        services.AddControllers();

        // Add services to the container.
        services.AddDbContext<DataContext>(opt =>
         {
             opt.UseSqlite(config.GetConnectionString("DefaultConnection"));

         });

        //opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))});
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();

        services.AddScoped<ITokenService, TokenService>();

        return services;


    }

}