using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class refactorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationStatus_StatusId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "FileRecord");

            migrationBuilder.DropIndex(
                name: "IX_Applications_StatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Contracts",
                table: "Direction");

            migrationBuilder.DropColumn(
                name: "FederalBudgets",
                table: "Direction");

            migrationBuilder.DropColumn(
                name: "LocalBudgets",
                table: "Direction");

            migrationBuilder.DropColumn(
                name: "SubjectsBudgets",
                table: "Direction");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Applications",
                newName: "CurrentStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ApplicationStatus",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationEntityId",
                table: "ApplicationStatus",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicationEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntity_Applications_ApplicationEntityId",
                        column: x => x.ApplicationEntityId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatus_ApplicationEntityId",
                table: "ApplicationStatus",
                column: "ApplicationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntity_ApplicationEntityId",
                table: "FileEntity",
                column: "ApplicationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatus_Applications_ApplicationEntityId",
                table: "ApplicationStatus",
                column: "ApplicationEntityId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatus_Applications_ApplicationEntityId",
                table: "ApplicationStatus");

            migrationBuilder.DropTable(
                name: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationStatus_ApplicationEntityId",
                table: "ApplicationStatus");

            migrationBuilder.DropColumn(
                name: "ApplicationEntityId",
                table: "ApplicationStatus");

            migrationBuilder.RenameColumn(
                name: "CurrentStatus",
                table: "Applications",
                newName: "StatusId");

            migrationBuilder.AddColumn<int>(
                name: "Contracts",
                table: "Direction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FederalBudgets",
                table: "Direction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalBudgets",
                table: "Direction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsBudgets",
                table: "Direction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ApplicationStatus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FileRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationRequestId = table.Column<int>(type: "integer", nullable: true),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileRecord_Applications_ApplicationRequestId",
                        column: x => x.ApplicationRequestId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StatusId",
                table: "Applications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FileRecord_ApplicationRequestId",
                table: "FileRecord",
                column: "ApplicationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationStatus_StatusId",
                table: "Applications",
                column: "StatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
