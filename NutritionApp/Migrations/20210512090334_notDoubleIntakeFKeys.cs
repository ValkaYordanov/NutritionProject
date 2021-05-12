using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class notDoubleIntakeFKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Meal_MealId",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Product_ProductId",
                table: "Intake");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Intake",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "Intake",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Meal_MealId",
                table: "Intake",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Product_ProductId",
                table: "Intake",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Meal_MealId",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Product_ProductId",
                table: "Intake");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Intake",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "Intake",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Meal_MealId",
                table: "Intake",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_Product_ProductId",
                table: "Intake",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
