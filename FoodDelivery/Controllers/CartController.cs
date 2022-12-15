using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var cart = _context.Carts.Include(x => x.CartFoods).ThenInclude(x => x.Food).FirstOrDefault(x => x.UserId == userId);
        var result = new CartViewModel
        {
            Foods = cart?.CartFoods.Select(x => new FoodViewModel
            {
                Id = x.Food.Id,
                Name = x.Food.Name,
                Price = x.Food.Price * x.Amount,
                Description = x.Food.Description,
                Amount = x.Amount
            }) ?? new List<FoodViewModel>(),
            TotalPrice = cart?.CartFoods.Sum(x => x.Food.Price * x.Amount) ?? 0
        };

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Buy()
    {
        var userId = _userManager.GetUserId(User);
        var cart = _context.Carts.Include(x => x.CartFoods).ThenInclude(x => x.Food).First(x => x.UserId == userId);
        var order = new Order
        {
            OrderStatusId = OrderStatus.New,
            FoodOrders = cart.CartFoods.Select(x => new FoodOrder { Food = x.Food, Amount = x.Amount }).ToList(),
            CustomerId = userId
        };

        _context.Orders.Add(order);
        _context.Carts.Remove(cart);

        _context.SaveChanges();

        return View();
    }
}