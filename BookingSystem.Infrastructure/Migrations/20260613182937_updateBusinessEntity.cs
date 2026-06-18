using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBusinessEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Businesses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Businesses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ApplicationUserId",
                table: "Businesses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_ApplicationUserId",
                table: "Businesses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_ApplicationUserId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_ApplicationUserId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Businesses");
        }
    }
}
