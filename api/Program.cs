using api.Data;
using Microsoft.OpenApi.Writers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddControllers();
  
// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt =>
{    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    
    });

//opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
 app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
