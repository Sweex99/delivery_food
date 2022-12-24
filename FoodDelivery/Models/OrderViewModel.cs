using FoodDelivery.Data;

namespace FoodDelivery.Models;

public class CourierViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class StatusNameViewModel
{
    public OrderStatus Id { get; set; }
    public string Name { get; set; }
}

public class OrderViewModel
{
    public int Id { get; set; }
    public int ItemsCount { get; set; }
    public IEnumerable<string> Items { get; set; }
    public string CustomerName { get; set; }
    public CourierViewModel? Courier { get; set; }
    public double Price { get; set; }
    public StatusNameViewModel Status { get; set; }
}