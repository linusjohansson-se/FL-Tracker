using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "game_stats",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kills = table.Column<int>(type: "integer", nullable: false),
                    assists = table.Column<int>(type: "integer", nullable: false),
                    deaths = table.Column<int>(type: "integer", nullable: false),
                    adr = table.Column<int>(type: "integer", nullable: false),
                    hs_ratio = table.Column<int>(type: "integer", nullable: false),
                    clutch_ratio = table.Column<int>(type: "integer", nullable: false),
                    fkpr = table.Column<int>(type: "integer", nullable: false),
                    fdpr = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_stats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    team_a = table.Column<Guid>(type: "uuid", nullable: false),
                    team_b = table.Column<Guid>(type: "uuid", nullable: false),
                    team_a_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    team_b_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    team_a_score = table.Column<int>(type: "integer", nullable: false),
                    team_b_score = table.Column<int>(type: "integer", nullable: false),
                    winner = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    steam_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leetify_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    faceit_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_stats",
                schema: "public");

            migrationBuilder.DropTable(
                name: "games",
                schema: "public");

            migrationBuilder.DropTable(
                name: "players",
                schema: "public");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "public");
        }
    }
}
