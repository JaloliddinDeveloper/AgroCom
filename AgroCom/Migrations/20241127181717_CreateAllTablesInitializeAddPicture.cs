using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroCom.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTablesInitializeAddPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPicture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OgitPicture",
                table: "Ogits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OgitPicture",
                table: "Ogits");
        }
    }
}
