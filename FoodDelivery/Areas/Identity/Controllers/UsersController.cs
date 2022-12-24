using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Areas.Identity.Controllers;

[Area("Identity")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();

        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = _userManager.Users.First(x => x.Id == userId);
        var result = new EditUserViewModel
        {
            Id = userId,
            IsLocked = user.LockoutEnabled,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
        };

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel viewModel)
    {
        var user = _userManager.Users.First(x => x.Id == viewModel.Id);
        await _userManager.RemoveFromRolesAsync(user, new[] { "Courier", "Admin" });

        if (!string.IsNullOrEmpty(viewModel.Role))
        {
            await _userManager.AddToRoleAsync(user, viewModel.Role);
        }

        await _userManager.SetLockoutEnabledAsync(user, viewModel.IsLocked);
        await _userManager.SetLockoutEndDateAsync(user, viewModel.IsLocked ? DateTimeOffset.MaxValue : null);

        return RedirectToAction(nameof(Index));
    }
}