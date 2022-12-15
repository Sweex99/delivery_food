namespace FoodDelivery.Data;

public enum OrderStatus
{
    New,
    InProgress,
    Delivered
}

public class LuOrderStatus
{
    public LuOrderStatus()
    {
        Orders = new HashSet<Order>();
    }

    public OrderStatus Id { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}