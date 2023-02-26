using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Migrations
{
    /// <inheritdoc />
    public partial class UtcOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtcOffsetMin",
                table: "Contests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtcOffsetMin",
                table: "Contests");
        }
    }
}
