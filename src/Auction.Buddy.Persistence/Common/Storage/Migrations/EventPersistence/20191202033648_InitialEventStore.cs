using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.Buddy.Persistence.Common.Storage.Migrations.EventPersistence
{
    public partial class InitialEventStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersistenceEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    AggregateId = table.Column<string>(nullable: false),
                    EventName = table.Column<string>(nullable: false),
                    SerializedEvent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistenceEvent", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersistenceEvent");
        }
    }
}
