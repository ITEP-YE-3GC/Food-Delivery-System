using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class CloseSprint1_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CustomizationNote = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NextStep = table.Column<int>(type: "integer", nullable: false),
                    Automated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "OrderTracking",
                columns: table => new
                {
                    OrderID = table.Column<long>(type: "bigint", nullable: false),
                    OrderStatusID = table.Column<long>(type: "bigint", nullable: false),
                    Seq = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTracking", x => new { x.OrderID, x.OrderStatusID });
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartCustomizations",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomizationID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartCustomizations", x => new { x.CartID, x.CustomizationID });
                    table.ForeignKey(
                        name: "FK_CartCustomizations_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerID = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantID = table.Column<int>(type: "integer", nullable: false),
                    DriverID = table.Column<int>(type: "integer", nullable: false),
                    AddressID = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentID = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusID = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalTable: "OrderStatus",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CustomizationNote = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "StatusID", "Automated", "Name", "NextStep" },
                values: new object[,]
                {
                    { 1, false, "Cancelled", 0 },
                    { 2, true, "Halted", 0 },
                    { 3, false, "Obstcles", 0 },
                    { 4, false, "Submitted", 1 },
                    { 5, false, "Received", 2 },
                    { 6, false, "Ready for Pick up", 3 },
                    { 7, false, "Picked up", 4 },
                    { 8, false, "On the way", 5 },
                    { 9, false, "Delivered", 6 }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "PaymentID", "Name" },
                values: new object[] { 1, "COD" });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerID_ProductID",
                table: "Carts",
                columns: new[] { "CustomerID", "ProductID" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentID",
                table: "Orders",
                column: "PaymentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartCustomizations");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderTracking");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
