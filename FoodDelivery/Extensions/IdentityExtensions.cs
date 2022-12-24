using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Extensions;

public static class IdentityExtensions
{
    public static async Task<bool> IsInRolesAsync(
        this UserManager<IdentityUser> userManager,
        ClaimsPrincipal principal,
        params string[] roles)
        => (await userManager.GetRolesAsync(await userManager.GetUserAsync(principal))).Any(roles.Contains);
}