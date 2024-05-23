using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventLocator.Migrations
{
    /// <inheritdoc />
    public partial class Event_Map_Position : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Position_X",
                table: "Events",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Position_Y",
                table: "Events",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position_X",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Position_Y",
                table: "Events");
        }
    }
}
