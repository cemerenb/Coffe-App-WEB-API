using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerifyEmailForgotPasswordTutorial.Migrations
{
    /// <inheritdoc />
    public partial class Menus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemCount = table.Column<int>(type: "int", nullable: false),
                    MenuItemPrice = table.Column<float>(type: "real", nullable: false),
                    MenuItemCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
