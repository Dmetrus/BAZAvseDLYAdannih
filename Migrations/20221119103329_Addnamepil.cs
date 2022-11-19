using Microsoft.EntityFrameworkCore.Migrations;

namespace US_5A_Net.Migrations
{
    public partial class Addnamepil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "namepil",
                table: "FlightersBD",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "namepil",
                table: "FlightersBD");
        }
    }
}
