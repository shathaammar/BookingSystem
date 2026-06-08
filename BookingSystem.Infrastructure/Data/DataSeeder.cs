using BookingSystem.Core.Entities;
using BookingSystem.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
                                           RoleManager<IdentityRole> roleManager)
        {
            // 1. Seed Roles
            string[] roles = { UserRoles.Admin, UserRoles.BusinessOwner, UserRoles.Customer };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Seed Admin User
            var adminEmail = "admin@test.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    FullName = "System Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }
        }
    }
}