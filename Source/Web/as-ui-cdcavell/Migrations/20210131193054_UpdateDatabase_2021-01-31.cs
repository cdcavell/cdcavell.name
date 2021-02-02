using Microsoft.EntityFrameworkCore.Migrations;

namespace as_ui_cdcavell.Migrations
{
    public partial class UpdateDatabase_20210131 : Migration
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
