using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveParkingSpaceApp.Migrations
{
    public partial class ScheduleFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SpotShift",
                table: "ParkingSpotReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ParkingSpotReservation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ParkingSpotReservation");

            migrationBuilder.AlterColumn<int>(
                name: "SpotShift",
                table: "ParkingSpotReservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
