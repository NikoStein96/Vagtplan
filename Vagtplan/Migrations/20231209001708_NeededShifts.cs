using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vagtplan.Migrations
{
    /// <inheritdoc />
    public partial class NeededShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftsNeeded",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftsNeeded",
                table: "Days");
        }
    }
}
