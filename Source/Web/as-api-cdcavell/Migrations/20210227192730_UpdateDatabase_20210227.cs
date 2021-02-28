using Microsoft.EntityFrameworkCore.Migrations;

namespace as_api_cdcavell.Migrations
{
    public partial class UpdateDatabase_20210227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Registration_ResourceId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Resource_ResourceId1",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_ResourceId1",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ResourceId1",
                table: "Role");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Resource_ResourceId",
                table: "Role",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Resource_ResourceId",
                table: "Role");

            migrationBuilder.AddColumn<long>(
                name: "ResourceId1",
                table: "Role",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_ResourceId1",
                table: "Role",
                column: "ResourceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Registration_ResourceId",
                table: "Role",
                column: "ResourceId",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Resource_ResourceId1",
                table: "Role",
                column: "ResourceId1",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
