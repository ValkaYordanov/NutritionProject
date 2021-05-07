using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class addSetEverywhere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Meal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Intake",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Intake",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Intake",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Intake");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Intake");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Intake");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ingredient");
        }
    }
}
