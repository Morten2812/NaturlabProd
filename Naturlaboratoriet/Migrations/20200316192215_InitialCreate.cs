using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Naturlaboratoriet.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_NaturLab_Admin",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NaturLab_Admin", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NaturLab_Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NaturLab_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NaturLab_Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    PrimaryImage = table.Column<string>(nullable: true),
                    SecondaryImage1 = table.Column<string>(nullable: true),
                    SecondaryImage2 = table.Column<string>(nullable: true),
                    SecondaryImage3 = table.Column<string>(nullable: true),
                    Instock = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NaturLab_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tbl_NaturLab_Product_tbl_NaturLab_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "tbl_NaturLab_Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_NaturLab_Product_CategoryID",
                table: "tbl_NaturLab_Product",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_NaturLab_Admin");

            migrationBuilder.DropTable(
                name: "tbl_NaturLab_Product");

            migrationBuilder.DropTable(
                name: "tbl_NaturLab_Category");
        }
    }
}
