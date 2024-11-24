using LazaRestaurant.Core.Application.Enums;
using LazaRestaurant.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace LazaRestaurant.Infrastructure.Identity.Seeds;

public static class DefaultServer
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser defaultServer = new ApplicationUser()
        {
            UserName = "server",
            Email = "defaultserver@gmail.com",
            FirstName = "Yael",
            LastName = "Ruiz",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultServer.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultServer.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultServer, "My_P4ssw0rd");
                await userManager.AddToRoleAsync(defaultServer, Roles.Server.ToString());
            }
        }
    }
}