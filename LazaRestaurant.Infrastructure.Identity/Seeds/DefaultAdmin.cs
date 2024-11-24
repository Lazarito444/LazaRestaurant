using LazaRestaurant.Core.Application.Enums;
using LazaRestaurant.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace LazaRestaurant.Infrastructure.Identity.Seeds;

public static class DefaultAdmin
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser defaultAdmin = new ApplicationUser()
        {
            UserName = "admin",
            Email = "defaultadmin@gmail.com",
            FirstName = "Tugo",
            LastName = "Sosa",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultAdmin, "My_P4ssw0rd");
                await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
            }
        }
    }
}