using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class addSetToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductNutritionId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNutritionId",
                table: "Product");
        }
    }
}
