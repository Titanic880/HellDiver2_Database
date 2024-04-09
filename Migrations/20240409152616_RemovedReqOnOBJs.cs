using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HellDiver2_API2DB.Migrations
{
    /// <inheritdoc />
    public partial class RemovedReqOnOBJs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dispatches",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispatches", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "eventDatas",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    eventType = table.Column<int>(type: "int", nullable: false),
                    faction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    health = table.Column<long>(type: "bigint", nullable: false),
                    maxHealth = table.Column<long>(type: "bigint", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    campaignId = table.Column<long>(type: "bigint", nullable: false),
                    joinOperationIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventDatas", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "rewards",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewards", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "statistics",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    missionsWon = table.Column<long>(type: "bigint", nullable: false),
                    missionsLost = table.Column<long>(type: "bigint", nullable: false),
                    missionTime = table.Column<long>(type: "bigint", nullable: false),
                    terminidKills = table.Column<long>(type: "bigint", nullable: false),
                    automatonKills = table.Column<long>(type: "bigint", nullable: false),
                    illuminateKills = table.Column<long>(type: "bigint", nullable: false),
                    bulletsFired = table.Column<long>(type: "bigint", nullable: false),
                    bulletsHit = table.Column<long>(type: "bigint", nullable: false),
                    timePlayed = table.Column<long>(type: "bigint", nullable: false),
                    deaths = table.Column<long>(type: "bigint", nullable: false),
                    revives = table.Column<long>(type: "bigint", nullable: false),
                    friendlies = table.Column<long>(type: "bigint", nullable: false),
                    missionSuccessRate = table.Column<long>(type: "bigint", nullable: false),
                    accuracy = table.Column<long>(type: "bigint", nullable: false),
                    playerCount = table.Column<long>(type: "bigint", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statistics", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "steamDatas",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_steamDatas", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "xyPositions",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    x = table.Column<double>(type: "float", nullable: false),
                    y = table.Column<double>(type: "float", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xyPositions", x => x.PK_id);
                });

            migrationBuilder.CreateTable(
                name: "assignmentDatas",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    briefing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FK_Task_ID = table.Column<long>(type: "bigint", nullable: false),
                    FK_Reward_ID = table.Column<long>(type: "bigint", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignmentDatas", x => x.PK_id);
                    table.ForeignKey(
                        name: "FK_assignmentDatas_rewards_FK_Reward_ID",
                        column: x => x.FK_Reward_ID,
                        principalTable: "rewards",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "warInfos",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    started = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ended = table.Column<DateTime>(type: "datetime2", nullable: false),
                    now = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clientVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    factions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    impactMultiplier = table.Column<double>(type: "float", nullable: false),
                    FK_Stats_ID = table.Column<long>(type: "bigint", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warInfos", x => x.PK_id);
                    table.ForeignKey(
                        name: "FK_warInfos_statistics_FK_Stats_ID",
                        column: x => x.FK_Stats_ID,
                        principalTable: "statistics",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    index = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hash = table.Column<long>(type: "bigint", nullable: false),
                    FK_Position_ID = table.Column<long>(type: "bigint", nullable: false),
                    waypoints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maxHealth = table.Column<long>(type: "bigint", nullable: false),
                    health = table.Column<long>(type: "bigint", nullable: false),
                    disabled = table.Column<bool>(type: "bit", nullable: false),
                    initialOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currentOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    regenPerSecond = table.Column<double>(type: "float", nullable: false),
                    FK_Events_ID = table.Column<long>(type: "bigint", nullable: true),
                    FK_Stats_ID = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planets", x => x.PK_id);
                    table.ForeignKey(
                        name: "FK_planets_eventDatas_FK_Events_ID",
                        column: x => x.FK_Events_ID,
                        principalTable: "eventDatas",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planets_statistics_FK_Stats_ID",
                        column: x => x.FK_Stats_ID,
                        principalTable: "statistics",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planets_xyPositions_FK_Position_ID",
                        column: x => x.FK_Position_ID,
                        principalTable: "xyPositions",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taskDatas",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valueTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FK_Task_ID = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskDatas", x => x.PK_id);
                    table.ForeignKey(
                        name: "FK_taskDatas_assignmentDatas_FK_Task_ID",
                        column: x => x.FK_Task_ID,
                        principalTable: "assignmentDatas",
                        principalColumn: "PK_id");
                });

            migrationBuilder.CreateTable(
                name: "campaign2s",
                columns: table => new
                {
                    PK_id = table.Column<long>(type: "bigint", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    FK_Planet_ID = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    DataEntryTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign2s", x => x.PK_id);
                    table.ForeignKey(
                        name: "FK_campaign2s_planets_FK_Planet_ID",
                        column: x => x.FK_Planet_ID,
                        principalTable: "planets",
                        principalColumn: "PK_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignmentDatas_FK_Reward_ID",
                table: "assignmentDatas",
                column: "FK_Reward_ID");

            migrationBuilder.CreateIndex(
                name: "IX_campaign2s_FK_Planet_ID",
                table: "campaign2s",
                column: "FK_Planet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_planets_FK_Events_ID",
                table: "planets",
                column: "FK_Events_ID");

            migrationBuilder.CreateIndex(
                name: "IX_planets_FK_Position_ID",
                table: "planets",
                column: "FK_Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_planets_FK_Stats_ID",
                table: "planets",
                column: "FK_Stats_ID");

            migrationBuilder.CreateIndex(
                name: "IX_taskDatas_FK_Task_ID",
                table: "taskDatas",
                column: "FK_Task_ID");

            migrationBuilder.CreateIndex(
                name: "IX_warInfos_FK_Stats_ID",
                table: "warInfos",
                column: "FK_Stats_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaign2s");

            migrationBuilder.DropTable(
                name: "dispatches");

            migrationBuilder.DropTable(
                name: "steamDatas");

            migrationBuilder.DropTable(
                name: "taskDatas");

            migrationBuilder.DropTable(
                name: "warInfos");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "assignmentDatas");

            migrationBuilder.DropTable(
                name: "eventDatas");

            migrationBuilder.DropTable(
                name: "statistics");

            migrationBuilder.DropTable(
                name: "xyPositions");

            migrationBuilder.DropTable(
                name: "rewards");
        }
    }
}
