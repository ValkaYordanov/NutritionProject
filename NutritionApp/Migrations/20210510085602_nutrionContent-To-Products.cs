using Microsoft.EntityFrameworkCore.Migrations;

namespace NutritionApp.Migrations
{
    public partial class nutrionContentToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductNutrition_ProductNutritionId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductNutrition");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductNutritionId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductNutritionId",
                table: "Product",
                newName: "Kcal");

            migrationBuilder.AddColumn<decimal>(
                name: "Carbs",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Fat",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FromCarbsSuggar",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FromFatSaturated",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "KJ",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Protein",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Salt",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FromCarbsSuggar",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FromFatSaturated",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "KJ",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Kcal",
                table: "Product",
                newName: "ProductNutritionId");

            migrationBuilder.CreateTable(
                name: "ProductNutrition",
                columns: table => new
                {
                    ProductNutritionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromCarbsSuggar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromFatSaturated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KJ = table.Column<int>(type: "int", nullable: false),
                    Kcal = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Salt = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNutrition", x => x.ProductNutritionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductNutritionId",
                table: "Product",
                column: "ProductNutritionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductNutrition_ProductNutritionId",
                table: "Product",
                column: "ProductNutritionId",
                principalTable: "ProductNutrition",
                principalColumn: "ProductNutritionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
