using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotellMenu.Migrations
{
    /// <inheritdoc />
    public partial class FixesWithSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomAvaliability",
                table: "HotelRooms",
                newName: "RoomAvailability");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomAvailability",
                table: "HotelRooms",
                newName: "RoomAvaliability");
        }
    }
}
