using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMenu.Persistence.Migrations
{
    public partial class RenameProductExtraToTopping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductToppings_Toppings_ProductExtraId",
                table: "ProductToppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductToppings",
                table: "ProductToppings");

            migrationBuilder.DropIndex(
                name: "IX_ProductToppings_ProductExtraId",
                table: "ProductToppings");

            migrationBuilder.DropColumn(
                name: "ProductExtraId",
                table: "ProductToppings");

            migrationBuilder.AddColumn<int>(
                name: "ToppingId",
                table: "ProductToppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductToppings",
                table: "ProductToppings",
                columns: new[] { "ProductId", "ToppingId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductToppings_ToppingId",
                table: "ProductToppings",
                column: "ToppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToppings_Toppings_ToppingId",
                table: "ProductToppings",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductToppings_Toppings_ToppingId",
                table: "ProductToppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductToppings",
                table: "ProductToppings");

            migrationBuilder.DropIndex(
                name: "IX_ProductToppings_ToppingId",
                table: "ProductToppings");

            migrationBuilder.DropColumn(
                name: "ToppingId",
                table: "ProductToppings");

            migrationBuilder.AddColumn<int>(
                name: "ProductExtraId",
                table: "ProductToppings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductToppings",
                table: "ProductToppings",
                columns: new[] { "ProductId", "ProductExtraId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductToppings_ProductExtraId",
                table: "ProductToppings",
                column: "ProductExtraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToppings_Toppings_ProductExtraId",
                table: "ProductToppings",
                column: "ProductExtraId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
