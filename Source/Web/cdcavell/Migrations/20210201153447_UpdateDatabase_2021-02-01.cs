using Microsoft.EntityFrameworkCore.Migrations;

namespace cdcavell.Migrations
{
    public partial class UpdateDatabase_20210201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Authorization",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Authorization");
        }
    }
}
