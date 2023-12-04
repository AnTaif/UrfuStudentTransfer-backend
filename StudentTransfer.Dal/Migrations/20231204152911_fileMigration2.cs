using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class fileMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ApplicationId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Files");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_ApplicationId",
                table: "Files",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
