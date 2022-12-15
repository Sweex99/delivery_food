using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartFood> CartFoods { get; set; }
    public virtual DbSet<Food> Foods { get; set; }
    public virtual DbSet<FoodOrder> FoodOrders { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<LuOrderStatus> LuOrderStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CartFood>(entity =>
        {
            entity.HasKey(x => new { x.CartId, x.FoodId });
        });

        builder.Entity<FoodOrder>(entity =>
        {
            entity.HasKey(x => new { x.FoodId, x.OrderId });
        });
    }
}