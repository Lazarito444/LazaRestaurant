using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LazaRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dishes_DishId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredients_IngredientId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDish_Orders_OrderId",
                table: "OrderDish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDish",
                table: "OrderDish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishIngredient",
                table: "DishIngredient");

            migrationBuilder.RenameTable(
                name: "OrderDish",
                newName: "OrderDishes");

            migrationBuilder.RenameTable(
                name: "DishIngredient",
                newName: "DishIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDish_DishId",
                table: "OrderDishes",
                newName: "IX_OrderDishes_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_IngredientId",
                table: "DishIngredients",
                newName: "IX_DishIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes",
                columns: new[] { "OrderId", "DishId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishIngredients",
                table: "DishIngredients",
                columns: new[] { "DishId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredients_Dishes_DishId",
                table: "DishIngredients",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredients_Ingredients_IngredientId",
                table: "DishIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDishes_Dishes_DishId",
                table: "OrderDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDishes_Orders_OrderId",
                table: "OrderDishes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredients_Dishes_DishId",
                table: "DishIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredients_Ingredients_IngredientId",
                table: "DishIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDishes_Dishes_DishId",
                table: "OrderDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDishes_Orders_OrderId",
                table: "OrderDishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishIngredients",
                table: "DishIngredients");

            migrationBuilder.RenameTable(
                name: "OrderDishes",
                newName: "OrderDish");

            migrationBuilder.RenameTable(
                name: "DishIngredients",
                newName: "DishIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDishes_DishId",
                table: "OrderDish",
                newName: "IX_OrderDish_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredients_IngredientId",
                table: "DishIngredient",
                newName: "IX_DishIngredient_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDish",
                table: "OrderDish",
                columns: new[] { "OrderId", "DishId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishIngredient",
                table: "DishIngredient",
                columns: new[] { "DishId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dishes_DishId",
                table: "DishIngredient",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredients_IngredientId",
                table: "DishIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDish_Orders_OrderId",
                table: "OrderDish",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
