using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTest.Migrations
{
    /// <inheritdoc />
    public partial class gg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_categoryId",
                table: "Items",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_categoryId",
                table: "Items",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_categoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_categoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Items");
        }
    }
}
