using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Data;

public class Order
{
    public Order()
    {
        FoodOrders = new HashSet<FoodOrder>();
    }

    public int Id { get; set; }
    public OrderStatus OrderStatusId { get; set; }
    public string CustomerId { get; set; }
    public string? CourierId { get; set; }

    public LuOrderStatus OrderStatus { get; set; } = null!;
    [ForeignKey(nameof(CustomerId))]
    public IdentityUser Customer { get; set; } = null!;
    [ForeignKey(nameof(CourierId))]
    public IdentityUser? Courier { get; set; } = null!;
    public ICollection<FoodOrder> FoodOrders { get; set; }
}