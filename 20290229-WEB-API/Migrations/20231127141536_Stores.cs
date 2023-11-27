using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20290229WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class Stores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreTaxId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreLogoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreCoverImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    StoreIsOn = table.Column<int>(type: "int", nullable: false),
                    StoreOpeningTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreClosingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),

                    StoreEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorePasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StorePasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StorePasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
