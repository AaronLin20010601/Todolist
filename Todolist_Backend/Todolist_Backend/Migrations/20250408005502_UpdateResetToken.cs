using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ResetTokens",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ResetTokens");
        }
    }
}
