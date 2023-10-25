using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedCarIdToAccident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReporterUserId",
                table: "Accidents",
                newName: "ApplicantUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicantUserId",
                table: "Accidents",
                newName: "ReporterUserId");
        }
    }
}
