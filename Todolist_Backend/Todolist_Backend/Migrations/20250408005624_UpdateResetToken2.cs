using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResetToken2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetTokens_Users_UserId",
                table: "ResetTokens");

            migrationBuilder.DropIndex(
                name: "IX_ResetTokens_UserId",
                table: "ResetTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ResetTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ResetTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResetTokens_UserId",
                table: "ResetTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetTokens_Users_UserId",
                table: "ResetTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
