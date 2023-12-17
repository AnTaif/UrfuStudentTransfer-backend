using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    /// <inheritdoc />
    public partial class userMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DetailedType",
                table: "Applications",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DetailedType",
                table: "Applications");
        }
    }
}
