using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordHashUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "Users",
                column: "Username");
        }
    }
}
