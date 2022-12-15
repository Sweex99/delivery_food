namespace FoodDelivery.Models;

public class FoodViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
}