using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20290229WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    ItemCanceled = table.Column<int>(type: "int", nullable: false),
                    CancelNote = table.Column<string>(type: "nvarchar(max)", nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "OrderDetails");
        }
    }
}
