using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveParkingSpaceApp.Migrations
{
    public partial class ScheduleFiles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ParkingSpotReservation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ParkingSpotReservation");
        }
    }
}
