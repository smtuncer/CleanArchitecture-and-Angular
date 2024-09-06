using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastracture.Data.Context;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.WebApi;
using CleanArchitecture.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
// Veritabaný baðlantýsý
string connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

// Identity ve DbContext ayarlarý
builder.Services.AddIdentity<User, Role>(action =>
{
    action.Password.RequiredLength = 1;
    action.Password.RequireUppercase = false;
    action.Password.RequireLowercase = false;
    action.Password.RequireNonAlphanumeric = false;
    action.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>();

// Middleware ve exception handling
builder.Services.AddTransient<ExceptionMiddleware>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

Helper.CreateUserAsync(app).Wait();

app.Run();
