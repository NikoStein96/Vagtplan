using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vagtplan.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeOrganisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_OrganisationId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganisationId",
                table: "Employees",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganisationId1",
                table: "Employees",
                column: "OrganisationId1",
                unique: true,
                filter: "[OrganisationId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Organisations_OrganisationId1",
                table: "Employees",
                column: "OrganisationId1",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Organisations_OrganisationId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OrganisationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OrganisationId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganisationId",
                table: "Employees",
                column: "OrganisationId",
                unique: true);
        }
    }
}
