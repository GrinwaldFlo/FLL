using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Migrations
{
    public partial class MigrationContestDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTo",
                table: "Contests",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateFrom",
                table: "Contests",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTo",
                table: "Contests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateFrom",
                table: "Contests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
