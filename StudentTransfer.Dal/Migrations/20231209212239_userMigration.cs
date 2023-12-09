using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class userMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId1",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AppUserId1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Applications");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppUserId",
                table: "Applications",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId",
                table: "Applications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AppUserId",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId1",
                table: "Applications",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppUserId1",
                table: "Applications",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId1",
                table: "Applications",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
