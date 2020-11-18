using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMenu.Persistence.Migrations
{
    public partial class AddBynaryPasswords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Cooks",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Cooks",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Admins",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Admins",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Cooks");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Cooks");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Admins");
        }
    }
}
