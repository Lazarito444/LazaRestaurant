using LazaRestaurant.Core.Application.Enums;
using LazaRestaurant.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace LazaRestaurant.Infrastructure.Identity.Seeds;

public static class DefaultSuperAdmin
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser defaultAdmin = new ApplicationUser()
        {
            UserName = "superadmin",
            Email = "superadmin@gmail.com",
            FirstName = "Ariel",
            LastName = "Lázaro",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultAdmin, "My_P4ssw0rd");
                await userManager.AddToRoleAsync(defaultAdmin, Roles.SuperAdmin.ToString());
                await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultAdmin, Roles.Server.ToString());
            }
        }
    }
}