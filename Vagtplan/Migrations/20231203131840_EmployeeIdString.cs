using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vagtplan.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeIdString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_EmployeeFirebaseId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeFirebaseId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "EmployeeFirebaseId",
                table: "Shifts");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Shifts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_EmployeeId",
                table: "Shifts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "FirebaseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Shifts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFirebaseId",
                table: "Shifts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeFirebaseId",
                table: "Shifts",
                column: "EmployeeFirebaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_EmployeeFirebaseId",
                table: "Shifts",
                column: "EmployeeFirebaseId",
                principalTable: "Employees",
                principalColumn: "FirebaseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
