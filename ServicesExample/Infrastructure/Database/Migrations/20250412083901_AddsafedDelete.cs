using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesExample.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddsafedDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "students",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "authors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "students");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "events");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "authors");
        }
    }
}
