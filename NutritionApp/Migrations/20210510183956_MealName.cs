using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class MealName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meal");

            migrationBuilder.AddColumn<string>(
                name: "MealName",
                table: "Meal",
                type: "varchar(300)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealName",
                table: "Meal");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meal",
                type: "varchar(500)",
                nullable: true);
        }
    }
}
