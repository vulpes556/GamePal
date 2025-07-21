using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePal.Migrations
{
    /// <inheritdoc />
    public partial class addedPlatformsToGamesEntitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Platforms",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_GameId",
                table: "Platforms",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Games_GameId",
                table: "Platforms",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Games_GameId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_GameId",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Platforms");
        }
    }
}
