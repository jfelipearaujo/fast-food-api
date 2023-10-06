using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedOrderItemProductRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_items_ProductId",
                table: "orders_items");

            migrationBuilder.CreateIndex(
                name: "IX_orders_items_ProductId",
                table: "orders_items",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_items_ProductId",
                table: "orders_items");

            migrationBuilder.CreateIndex(
                name: "IX_orders_items_ProductId",
                table: "orders_items",
                column: "ProductId",
                unique: true);
        }
    }
}
