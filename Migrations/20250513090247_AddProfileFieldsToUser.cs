using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ADDRESS",
                table: "USER",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EMAILNOTIFICATIONS",
                table: "USER",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PHONENUMBER",
                table: "USER",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PREFERREDLANGUAGE",
                table: "USER",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PROFILEIMAGEURL",
                table: "USER",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SMSNOTIFICATIONS",
                table: "USER",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDRESS",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "EMAILNOTIFICATIONS",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "PHONENUMBER",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "PREFERREDLANGUAGE",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "PROFILEIMAGEURL",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "SMSNOTIFICATIONS",
                table: "USER");
        }
    }
}
