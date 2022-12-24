namespace FoodDelivery.Models;

public class EditUserViewModel
{
    public string Id { get; set; }
    public string? Role { get; set; }
    public bool IsLocked { get; set; }
}