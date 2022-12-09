using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Data;

public static class IdentitySeed
{
    // Run only once
    public static async Task CreateDefaultIdentity(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var courierRole = new IdentityRole { Name = "Courier" };
        var adminRole = new IdentityRole { Name = "Admin" };

        await roleManager.CreateAsync(courierRole);
        await roleManager.CreateAsync(adminRole);

        var admin = new IdentityUser
        {
            UserName = "anon@email.com",
            Email = "anon@email.com",
        };

        await userManager.CreateAsync(admin, "tempPassword123!@#");
        await userManager.AddToRoleAsync(admin, adminRole.Name);
    }
}