using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD2_EFDatabase.Migrations
{
    /// <inheritdoc />
    public partial class WarInfo_nullableStatsID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_warInfos_statistics_FK_Stats_ID",
                table: "warInfos");

            migrationBuilder.AlterColumn<long>(
                name: "FK_Stats_ID",
                table: "warInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_warInfos_statistics_FK_Stats_ID",
                table: "warInfos",
                column: "FK_Stats_ID",
                principalTable: "statistics",
                principalColumn: "PK_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_warInfos_statistics_FK_Stats_ID",
                table: "warInfos");

            migrationBuilder.AlterColumn<long>(
                name: "FK_Stats_ID",
                table: "warInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_warInfos_statistics_FK_Stats_ID",
                table: "warInfos",
                column: "FK_Stats_ID",
                principalTable: "statistics",
                principalColumn: "PK_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
