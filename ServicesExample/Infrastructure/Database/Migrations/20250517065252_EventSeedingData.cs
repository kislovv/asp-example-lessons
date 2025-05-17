using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesExample.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class EventSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("events",
                new []
                {
                    "id", 
                    "name", 
                    "place", 
                    "date_time_of_start",
                    "date_time_of_end",
                    "quota",
                    "score",
                    "author_id",
                    "is_deleted",
                }, 
                new object[,]
                {
                    {
                        Guid.NewGuid(), 
                        "День России", 
                        "Кремлевская 35, к2",
                        new DateTimeOffset(new DateTime(2025, 5, 16, 12, 0,0)),
                        new DateTimeOffset(new DateTime(2025, 5, 16, 15, 0,0)),
                        100,
                        10,
                        1,
                        false,
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
