using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.WebApi;

public static class Helper
{
    public static async Task CreateUserAsync(WebApplication app)
    {
        //Hiç kullanıcı yoksa kullanıcı oluşturur
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(new()
                {
                    FirstName = "Selçuk",
                    LastName = "TUNÇER",
                    Email = "admin@gmail.com",
                    UserName = "admin",
                }, "1");

            }
        }
    }
}
