using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Data;

public class FoodOrder
{
    public int FoodId { get; set; }
    public int OrderId { get; set; }
    public int Amount { get; set; }

    public Food Food { get; set; } = null!;
    public Order Order { get; set; } = null!;
}