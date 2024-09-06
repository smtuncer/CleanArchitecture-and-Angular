using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Data.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
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

// Controllers ekleniyor
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReferance).Assembly);

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

// Exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

// Authentication ve Authorization
app.UseAuthentication();  // Authentication eklendi
app.UseAuthorization();

app.MapControllers();

app.Run();
