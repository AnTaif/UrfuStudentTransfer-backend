using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class fileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId",
                table: "FileEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Applications_ApplicationId1",
                table: "FileEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity");

            migrationBuilder.RenameTable(
                name: "FileEntity",
                newName: "Files");

            migrationBuilder.RenameColumn(
                name: "ApplicationId1",
                table: "Files",
                newName: "ApplicationEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FileEntity_ApplicationId1",
                table: "Files",
                newName: "IX_Files_ApplicationEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FileEntity_ApplicationId",
                table: "Files",
                newName: "IX_Files_ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Applications_ApplicationEntityId",
                table: "Files",
                column: "ApplicationEntityId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Applications_ApplicationEntityId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileEntity");

            migrationBuilder.RenameColumn(
                name: "ApplicationEntityId",
                table: "FileEntity",
                newName: "ApplicationId1");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ApplicationId",
                table: "FileEntity",
                newName: "IX_FileEntity_ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ApplicationEntityId",
                table: "FileEntity",
                newName: "IX_FileEntity_ApplicationId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity",
                column: "Id");

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
    }
}
