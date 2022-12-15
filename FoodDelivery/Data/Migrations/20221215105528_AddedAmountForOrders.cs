using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Data.Migrations
{
    public partial class AddedAmountForOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "FoodOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "CartFoods",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "FoodOrders");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CartFoods");
        }
    }
}
