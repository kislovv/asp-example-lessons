using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesExample.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AdduserLoginIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_students_login",
                table: "students",
                column: "login");

            migrationBuilder.CreateIndex(
                name: "IX_authors_login",
                table: "authors",
                column: "login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_students_login",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_authors_login",
                table: "authors");
        }
    }
}
