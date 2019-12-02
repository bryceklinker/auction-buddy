using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.Buddy.Persistence.Common.Storage.Migrations.ReadStore
{
    public partial class InitialReadStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionReadModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    AuctionDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItemReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuctionId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: true),
                    Donor = table.Column<string>(maxLength: 256, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItemReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionItemReadModel_AuctionReadModel_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItemReadModel_AuctionId",
                table: "AuctionItemReadModel",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionItemReadModel");

            migrationBuilder.DropTable(
                name: "AuctionReadModel");
        }
    }
}
