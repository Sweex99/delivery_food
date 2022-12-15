using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Data;

public class Cart
{
    public Cart()
    {
        CartFoods = new HashSet<CartFood>();
    }

    [Key]
    public string UserId { get; set; }

    public ICollection<CartFood> CartFoods { get; set; }
    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; } = null!;
}