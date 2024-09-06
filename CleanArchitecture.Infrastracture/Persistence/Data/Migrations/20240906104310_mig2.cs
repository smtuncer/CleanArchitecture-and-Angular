using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class mig2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_UserRole",
            table: "UserRole");

        migrationBuilder.RenameTable(
            name: "UserRole",
            newName: "UserRoles");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserRoles",
            table: "UserRoles",
            columns: new[] { "UserId", "RoleId" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_UserRoles",
            table: "UserRoles");

        migrationBuilder.RenameTable(
            name: "UserRoles",
            newName: "UserRole");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserRole",
            table: "UserRole",
            columns: new[] { "UserId", "RoleId" });
    }
}
