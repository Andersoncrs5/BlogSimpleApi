using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSimpleApi.Migrations
{
    /// <inheritdoc />
    public partial class ThirdUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "EmailUser",
                table: "Posts",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlockByUser",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmailUser",
                table: "Comments",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlockByPost",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlockByUser",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailUser",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsBlockByUser",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EmailUser",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsBlockByPost",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsBlockByUser",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);
        }
    }
}
