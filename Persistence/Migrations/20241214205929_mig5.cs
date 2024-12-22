using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments");

            migrationBuilder.DropTable(
                name: "BrandsCategories");

            migrationBuilder.DropColumn(
                name: "BrandsId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Brands");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments",
                column: "ProductsId",
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

            migrationBuilder.AddColumn<int>(
                name: "BrandsId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BrandsCategories",
                columns: table => new
                {
                    BrandsId = table.Column<int>(type: "int", nullable: false),
                    categoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandsCategories", x => new { x.BrandsId, x.categoriesId });
                    table.ForeignKey(
                        name: "FK_BrandsCategories_Brands_BrandsId",
                        column: x => x.BrandsId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandsCategories_Categories_categoriesId",
                        column: x => x.categoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandsCategories_categoriesId",
                table: "BrandsCategories",
                column: "categoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Products_ProductsId",
                table: "comments",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
