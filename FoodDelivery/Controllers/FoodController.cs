using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers;

public class FoodController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public FoodController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var result = _context.Foods.Select(x => new FoodViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Description = x.Description
        });

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult ToCart(int id)
    {
        var userId = _userManager.GetUserId(User);
        var cart = _context.Carts.Include(x => x.CartFoods).FirstOrDefault(x => x.UserId == userId);
        var selectedFood = _context.Foods.First(x => x.Id == id);

        if (cart is null)
        {
            cart = new Cart
            {
                UserId = userId
            };
            _context.Carts.Add(cart);
        }

        var cartFood = cart.CartFoods.FirstOrDefault(x => x.FoodId == selectedFood.Id);

        if (cartFood is not null)
        {
            cartFood.Amount++;
            _context.CartFoods.Update(cartFood);
        }
        else
        {
            cart.CartFoods.Add(new CartFood { Food = selectedFood, Amount = 1 });
        }

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}