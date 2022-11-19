using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace US_5A_Net.Migrations
{
    public partial class MigBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightersBD",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    numflight = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    eta = table.Column<DateTime>(nullable: false),
                    countPas = table.Column<int>(nullable: false),
                    pricePas = table.Column<double>(nullable: false),
                    countCrew = table.Column<int>(nullable: false),
                    priceCrew = table.Column<double>(nullable: false),
                    procDop = table.Column<double>(nullable: false),
                    sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightersBD", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightersBD");
        }
    }
}
