using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace as_api_cdcavell.Migrations
{
    public partial class UpdateDatabase_20210309 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidationDate",
                table: "Registration",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidationToken",
                table: "Registration",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationDate",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "ValidationToken",
                table: "Registration");
        }
    }
}
