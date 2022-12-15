using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Data.Migrations
{
    public partial class AddedDescriptionForFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Foods",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Foods");
        }
    }
}
