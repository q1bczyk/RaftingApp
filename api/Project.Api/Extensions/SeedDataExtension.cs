using Microsoft.AspNetCore.Identity;
using Project.Api.Seeder;

namespace Project.Api.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
                await IdentityRolesSeeder.SeedRolesAsync(roleManager);
            }
        }
    }
}