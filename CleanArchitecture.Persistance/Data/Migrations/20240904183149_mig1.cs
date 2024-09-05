using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Persistance.Migrations;

/// <inheritdoc />
public partial class mig1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BlogCategories",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                BlogCategoryImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsPublished = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BlogCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Blogs",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BlogCategoryId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                BlogImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CommentsEnabled = table.Column<bool>(type: "bit", nullable: false),
                IsPublished = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Blogs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Blogs_BlogCategories_BlogCategoryId1",
                    column: x => x.BlogCategoryId1,
                    principalTable: "BlogCategories",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Blogs_BlogCategoryId1",
            table: "Blogs",
            column: "BlogCategoryId1");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Blogs");

        migrationBuilder.DropTable(
            name: "BlogCategories");
    }
}
