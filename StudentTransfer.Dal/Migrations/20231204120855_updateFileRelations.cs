using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class updateFileRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Applications_ApplicationEntityId",
                table: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_FileEntity_ApplicationEntityId",
                table: "FileEntity");

            migrationBuilder.DropColumn(
                name: "ApplicationEntityId",
                table: "FileEntity");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "FileEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId1",
                table: "FileEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_ApplicationId",
                table: "FileEntity",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_ApplicationId1",
                table: "FileEntity",
                column: "ApplicationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId",
                table: "FileEntity",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId1",
                table: "FileEntity",
                column: "ApplicationId1",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId",
                table: "FileEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId1",
                table: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_FileEntity_ApplicationId",
                table: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_FileEntity_ApplicationId1",
                table: "FileEntity");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "FileEntity");

            migrationBuilder.DropColumn(
                name: "ApplicationId1",
                table: "FileEntity");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationEntityId",
                table: "FileEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_ApplicationEntityId",
                table: "FileEntity",
                column: "ApplicationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntity_Applications_ApplicationEntityId",
                table: "FileEntity",
                column: "ApplicationEntityId",
                principalTable: "Applications",
                principalColumn: "Id");
        }
    }
}
