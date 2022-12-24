using FoodDelivery.Data;
using FoodDelivery.Extensions;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers;

public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Courier,Admin")]
    public async Task<IActionResult> Index()
    {
        var query = _context
            .Orders
            .Include(x => x.Courier)
            .Include(x => x.OrderStatus)
            .Include(x => x.Customer)
            .Include(x => x.FoodOrders)
            .ThenInclude(x => x.Food)
            .AsQueryable();

        if (await _userManager.IsInRolesAsync(User, "Courier"))
        {
            query = query.Where(x => x.OrderStatusId != OrderStatus.Delivered);
        }

        var result = await query
            .Select(x => new OrderViewModel
            {
                Id = x.Id,
                CustomerName = x.Customer.UserName,
                Courier = x.Courier != null ? new() { Id = x.Courier.Id, Name = x.Courier.UserName } : null,
                Status = new() { Id = x.OrderStatus.Id, Name = x.OrderStatus.Name },
                ItemsCount = x.FoodOrders.Count,
                Price = x.FoodOrders.Sum(foodOrder => foodOrder.Food.Price * foodOrder.Amount),
                Items = x.FoodOrders.Select(foodOrder => $"{foodOrder.Food.Name} x{foodOrder.Amount}")
            })
            .ToListAsync();

        return View(result);
    }

    [HttpPost]
    [Authorize(Roles = "Courier")]
    public IActionResult Take(int id)
    {
        var userId = _userManager.GetUserId(User);
        var order = _context.Orders.First(x => x.Id == id);
        order.CourierId = userId;
        order.OrderStatusId = OrderStatus.InProgress;

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Courier")]
    public IActionResult Delivered(int id)
    {
        var order = _context.Orders.First(x => x.Id == id);
        order.OrderStatusId = OrderStatus.Delivered;

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}