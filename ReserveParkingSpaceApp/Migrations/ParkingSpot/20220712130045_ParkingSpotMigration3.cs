using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveParkingSpaceApp.Migrations.ParkingSpot
{
    public partial class ParkingSpotMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<int>(
                name: "SpotShift",
                table: "ParkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotShift",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "ParkingSpots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
