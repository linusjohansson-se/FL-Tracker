using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldsForGameStat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "clutch_ratio",
                schema: "public",
                table: "game_stats",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "clutch_attempts",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "clutch_wins",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "first_deaths",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "first_kills",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "headshot_kills",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rounds_played",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_damage",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clutch_attempts",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "clutch_wins",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "first_deaths",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "first_kills",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "headshot_kills",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "rounds_played",
                schema: "public",
                table: "game_stats");

            migrationBuilder.DropColumn(
                name: "total_damage",
                schema: "public",
                table: "game_stats");

            migrationBuilder.AlterColumn<int>(
                name: "clutch_ratio",
                schema: "public",
                table: "game_stats",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
