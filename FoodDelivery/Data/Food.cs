namespace FoodDelivery.Data;

public class Food
{
    public Food()
    {
        FoodOrders = new HashSet<FoodOrder>();
        CartFoods = new HashSet<CartFood>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }

    public ICollection<FoodOrder> FoodOrders { get; set; }
    public ICollection<CartFood> CartFoods { get; set; }
}