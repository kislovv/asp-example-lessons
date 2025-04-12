using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesExample.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "UserSequence");

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"UserSequence\"')"),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"UserSequence\"')"),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    course = table.Column<long>(type: "bigint", nullable: false),
                    score = table.Column<long>(type: "bigint", nullable: false),
                    group = table.Column<string>(type: "text", nullable: false),
                    institute = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    place = table.Column<string>(type: "text", nullable: false),
                    date_time_of_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_time_of_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quota = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    score = table.Column<long>(type: "bigint", nullable: false),
                    author_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_events_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_student",
                columns: table => new
                {
                    events_id = table.Column<Guid>(type: "uuid", nullable: false),
                    students_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_student", x => new { x.events_id, x.students_id });
                    table.ForeignKey(
                        name: "fk_event_student_events_events_id",
                        column: x => x.events_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_student_students_students_id",
                        column: x => x.students_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_event_student_students_id",
                table: "event_student",
                column: "students_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_author_id",
                table: "events",
                column: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_student");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropSequence(
                name: "UserSequence");
        }
    }
}
