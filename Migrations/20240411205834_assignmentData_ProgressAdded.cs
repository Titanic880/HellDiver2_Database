using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD2_EFDatabase.Migrations
{
    /// <inheritdoc />
    public partial class assignmentData_ProgressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "progress",
                table: "assignmentDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "progress",
                table: "assignmentDatas");
        }
    }
}
