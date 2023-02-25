using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Migrations
{
    /// <inheritdoc />
    public partial class Matches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContestMatches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestMatches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContestMatches_Contests_ContestID",
                        column: x => x.ContestID,
                        principalTable: "Contests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MinBtwMatch = table.Column<double>(type: "double", nullable: false),
                    ContestMatchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rounds_ContestMatches_ContestMatchID",
                        column: x => x.ContestMatchID,
                        principalTable: "ContestMatches",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContestMatchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tables_ContestMatches_ContestMatchID",
                        column: x => x.ContestMatchID,
                        principalTable: "ContestMatches",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeamItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    School = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContestMatchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeamItem_ContestMatches_ContestMatchID",
                        column: x => x.ContestMatchID,
                        principalTable: "ContestMatches",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TableID = table.Column<int>(type: "int", nullable: true),
                    Team1ID = table.Column<int>(type: "int", nullable: true),
                    Team2ID = table.Column<int>(type: "int", nullable: true),
                    RoundItemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Rounds_RoundItemID",
                        column: x => x.RoundItemID,
                        principalTable: "Rounds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_Tables_TableID",
                        column: x => x.TableID,
                        principalTable: "Tables",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_TeamItem_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "TeamItem",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_TeamItem_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "TeamItem",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ContestMatches_ContestID",
                table: "ContestMatches",
                column: "ContestID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RoundItemID",
                table: "Matches",
                column: "RoundItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TableID",
                table: "Matches",
                column: "TableID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1ID",
                table: "Matches",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2ID",
                table: "Matches",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_ContestMatchID",
                table: "Rounds",
                column: "ContestMatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ContestMatchID",
                table: "Tables",
                column: "ContestMatchID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamItem_ContestMatchID",
                table: "TeamItem",
                column: "ContestMatchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "TeamItem");

            migrationBuilder.DropTable(
                name: "ContestMatches");
        }
    }
}
