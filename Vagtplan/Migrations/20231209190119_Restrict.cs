using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vagtplan.Migrations
{
    /// <inheritdoc />
    public partial class Restrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OrganisationId",
                table: "Schedules",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Organisations_OrganisationId",
                table: "Schedules",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Organisations_OrganisationId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_OrganisationId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Schedules");
        }
    }
}
