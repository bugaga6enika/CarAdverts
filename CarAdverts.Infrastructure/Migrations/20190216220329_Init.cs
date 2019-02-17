using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAdverts.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarAdverts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    New = table.Column<bool>(nullable: false),
                    Mileage = table.Column<int>(nullable: true),
                    FirstRegistration = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAdverts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarAdverts");
        }
    }
}
