using FoodDelivery.Data;

namespace FoodDelivery.Models;

public class OrderViewModel
{
    public int Id { get; set; }
    public int ItemsCount { get; set; }
    public IEnumerable<string> Items { get; set; }
    public string CustomerName { get; set; }
    public string? CourierName { get; set; }
    public double Price { get; set; }
    public OrderStatus Status { get; set; }
}