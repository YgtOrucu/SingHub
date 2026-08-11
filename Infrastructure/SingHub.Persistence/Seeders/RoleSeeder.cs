using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.Seeders;

public static class RoleSeeder
{
    public static async Task SeedRolesAndAdminUserAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        string[] roles = ["Standart", "Basic", "Gold", "Premium", "Elit"];

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }

        string adminEmail = "orucuyigit@gmail.com";
        string adminUserName = "admin";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new AppUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                Name = "System",
                Surname = "Admin",
                EmailConfirmed = true,
            };

            var createResult = await userManager.CreateAsync(newAdmin, "Admin123!*");

            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Standart");
            }
            else
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception($"Admin kullanıcısı oluşturulamadı: {errors}");
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(adminUser, "Standart"))
            {
                await userManager.AddToRoleAsync(adminUser, "Standart");
            }
        }
    }
}