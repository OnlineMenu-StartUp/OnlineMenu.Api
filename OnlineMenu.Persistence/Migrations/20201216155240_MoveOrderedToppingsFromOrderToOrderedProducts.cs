using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMenu.Persistence.Migrations
{
    public partial class MoveOrderedToppingsFromOrderToOrderedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProductExtras_Orders_OrderId",
                table: "OrderedProductExtras");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProductExtras_OrderId",
                table: "OrderedProductExtras");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderedProductExtras");

            migrationBuilder.AddColumn<int>(
                name: "OrderedProductId",
                table: "OrderedProductExtras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProductExtras_OrderedProductId",
                table: "OrderedProductExtras",
                column: "OrderedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProductExtras_OrderedProducts_OrderedProductId",
                table: "OrderedProductExtras",
                column: "OrderedProductId",
                principalTable: "OrderedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProductExtras_OrderedProducts_OrderedProductId",
                table: "OrderedProductExtras");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProductExtras_OrderedProductId",
                table: "OrderedProductExtras");

            migrationBuilder.DropColumn(
                name: "OrderedProductId",
                table: "OrderedProductExtras");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderedProductExtras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProductExtras_OrderId",
                table: "OrderedProductExtras",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProductExtras_Orders_OrderId",
                table: "OrderedProductExtras",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
