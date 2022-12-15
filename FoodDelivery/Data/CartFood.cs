namespace FoodDelivery.Data;

public class CartFood
{
    public int CartId { get; set; }
    public int FoodId { get; set; }
    public int Amount { get; set; }

    public Cart Cart { get; set; } = null!;
    public Food Food { get; set; } = null!;
}