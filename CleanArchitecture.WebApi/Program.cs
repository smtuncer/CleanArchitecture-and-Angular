using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Persistance.Data.Context;
using CleanArchitecture.Infrastructure.Persistance.Repositories;
using CleanArchitecture.WebApi.Middleware;
using CleanArcihtecture.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection yapýlandýrmasý
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IGenericRepository<BlogCategory>, GenericRepository<BlogCategory, AppDbContext>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// MediatR ve FluentValidation pipeline davranýþý
builder.Services.AddMediatR(crf =>
    crf.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReferance).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReferance).Assembly);

// Swagger yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliþtirme ortamýnda Swagger kullanýmý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
