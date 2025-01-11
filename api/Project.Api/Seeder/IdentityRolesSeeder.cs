using Microsoft.AspNetCore.Identity;

namespace Project.Api.Seeder
{
    public static class IdentityRolesSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<string>> roleManager)
        {
            var roles = new[] { "admin", "employee" }; 

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}