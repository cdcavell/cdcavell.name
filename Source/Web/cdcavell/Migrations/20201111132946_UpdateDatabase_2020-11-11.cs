using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cdcavell.Migrations
{
    public partial class UpdateDatabase_20201111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Registration",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Registration",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Registration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevokedBy",
                table: "Registration",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedDate",
                table: "Registration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RevokedBy",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RevokedDate",
                table: "Registration");
        }
    }
}
