using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersProducts_Products_productsId",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "roles",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersProducts_Products_productsId",
                table: "OrdersProducts",
                column: "productsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersProducts_Products_productsId",
                table: "OrdersProducts");

            migrationBuilder.AddColumn<string>(
                name: "roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersProducts_Products_productsId",
                table: "OrdersProducts",
                column: "productsId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
