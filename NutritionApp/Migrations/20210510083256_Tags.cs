using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Meal",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Intake",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductNutritionId",
                table: "Product",
                column: "ProductNutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_UserId",
                table: "Meal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Intake_MealId",
                table: "Intake",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Intake_ProductId",
                table: "Intake",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Intake_UserId",
                table: "Intake",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ProductId",
                table: "Ingredient",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Product_ProductId",
                table: "Ingredient",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Intake_AspNetUsers_UserId",
                table: "Intake",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_AspNetUsers_UserId",
                table: "Meal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductNutrition_ProductNutritionId",
                table: "Product",
                column: "ProductNutritionId",
                principalTable: "ProductNutrition",
                principalColumn: "ProductNutritionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Product_ProductId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Intake_AspNetUsers_UserId",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Meal_MealId",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Intake_Product_ProductId",
                table: "Intake");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_AspNetUsers_UserId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductNutrition_ProductNutritionId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductNutritionId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Meal_UserId",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Intake_MealId",
                table: "Intake");

            migrationBuilder.DropIndex(
                name: "IX_Intake_ProductId",
                table: "Intake");

            migrationBuilder.DropIndex(
                name: "IX_Intake_UserId",
                table: "Intake");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_ProductId",
                table: "Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Meal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Intake",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
