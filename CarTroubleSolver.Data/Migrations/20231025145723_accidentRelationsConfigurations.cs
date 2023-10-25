using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Data.Migrations
{
    /// <inheritdoc />
    public partial class accidentRelationsConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accidents_Cars_CarId",
                table: "Accidents");

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_ApplicantUserId",
                table: "Accidents",
                column: "ApplicantUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accidents_Cars_CarId",
                table: "Accidents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accidents_Users_ApplicantUserId",
                table: "Accidents",
                column: "ApplicantUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accidents_Cars_CarId",
                table: "Accidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Accidents_Users_ApplicantUserId",
                table: "Accidents");

            migrationBuilder.DropIndex(
                name: "IX_Accidents_ApplicantUserId",
                table: "Accidents");

            migrationBuilder.AddForeignKey(
                name: "FK_Accidents_Cars_CarId",
                table: "Accidents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
