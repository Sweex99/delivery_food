using FoodDelivery.Data;
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
    [Authorize(Roles = "Courier")]
    public IActionResult Index()
    {
        var result = _context
            .Orders
            .Include(x => x.Courier)
            .Include(x => x.Customer)
            .Include(x => x.FoodOrders)
            .ThenInclude(x => x.Food)
            .Where(x => x.OrderStatusId != OrderStatus.Delivered)
            .Select(x => new OrderViewModel
            {
                Id = x.Id,
                CustomerName = x.Customer.UserName,
                CourierName = x.Courier.UserName,
                Status = x.OrderStatusId,
                ItemsCount = x.FoodOrders.Count,
                Price = x.FoodOrders.Sum(foodOrder => foodOrder.Food.Price * foodOrder.Amount),
                Items = x.FoodOrders.Select(foodOrder => $"{foodOrder.Food.Name} x{foodOrder.Amount}")
            });

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