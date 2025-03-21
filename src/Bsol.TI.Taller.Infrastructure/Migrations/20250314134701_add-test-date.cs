using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsol.TI.Taller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtestdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TestAt",
                table: "Tests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestAt",
                table: "Tests");
        }
    }
}
