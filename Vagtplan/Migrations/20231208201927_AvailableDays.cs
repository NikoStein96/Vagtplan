using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vagtplan.Migrations
{
    /// <inheritdoc />
    public partial class AvailableDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferedWorkDays");

            migrationBuilder.CreateTable(
                name: "DayEmployee",
                columns: table => new
                {
                    AvailableEmployeesFirebaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreferedWorkDaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayEmployee", x => new { x.AvailableEmployeesFirebaseId, x.PreferedWorkDaysId });
                    table.ForeignKey(
                        name: "FK_DayEmployee_Days_PreferedWorkDaysId",
                        column: x => x.PreferedWorkDaysId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayEmployee_Employees_AvailableEmployeesFirebaseId",
                        column: x => x.AvailableEmployeesFirebaseId,
                        principalTable: "Employees",
                        principalColumn: "FirebaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayEmployee_PreferedWorkDaysId",
                table: "DayEmployee",
                column: "PreferedWorkDaysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayEmployee");

            migrationBuilder.CreateTable(
                name: "PreferedWorkDays",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weekday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferedWorkDays", x => new { x.EmployeeId, x.Weekday });
                    table.ForeignKey(
                        name: "FK_PreferedWorkDays_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "FirebaseId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
