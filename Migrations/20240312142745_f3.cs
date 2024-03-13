using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class f3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_OrderID",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_OrderID",
                table: "orderDetails");

            migrationBuilder.AddColumn<long>(
                name: "OrdersOrderID",
                table: "orderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_OrdersOrderID",
                table: "orderDetails",
                column: "OrdersOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_OrdersOrderID",
                table: "orderDetails",
                column: "OrdersOrderID",
                principalTable: "orders",
                principalColumn: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_OrdersOrderID",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_OrdersOrderID",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "OrdersOrderID",
                table: "orderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_OrderID",
                table: "orderDetails",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_OrderID",
                table: "orderDetails",
                column: "OrderID",
                principalTable: "orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
