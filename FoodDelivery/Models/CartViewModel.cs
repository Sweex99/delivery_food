namespace FoodDelivery.Models;

public class CartViewModel
{
    public IEnumerable<FoodViewModel> Foods { get; set; }
    public double TotalPrice { get; set; }
}