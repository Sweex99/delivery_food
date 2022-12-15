using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Data.Migrations
{
    public partial class FixedUserIdTypeInCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartFoods_Carts_CartId",
                table: "CartFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId1",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId1",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CartUserId",
                table: "CartFoods",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CartFoods_CartUserId",
                table: "CartFoods",
                column: "CartUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartFoods_Carts_CartUserId",
                table: "CartFoods",
                column: "CartUserId",
                principalTable: "Carts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartFoods_Carts_CartUserId",
                table: "CartFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartFoods_CartUserId",
                table: "CartFoods");

            migrationBuilder.DropColumn(
                name: "CartUserId",
                table: "CartFoods");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Carts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId1",
                table: "Carts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartFoods_Carts_CartId",
                table: "CartFoods",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId1",
                table: "Carts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
