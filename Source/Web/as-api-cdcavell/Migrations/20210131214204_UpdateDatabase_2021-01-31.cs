using Microsoft.EntityFrameworkCore.Migrations;

namespace as_api_cdcavell.Migrations
{
    public partial class UpdateDatabase_20210131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Registration_ApprovedById",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Registration_RevokedById",
                table: "Registration");

            migrationBuilder.AlterColumn<long>(
                name: "RevokedById",
                table: "Registration",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedById",
                table: "Registration",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Registration_ApprovedById",
                table: "Registration",
                column: "ApprovedById",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Registration_RevokedById",
                table: "Registration",
                column: "RevokedById",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Registration_ApprovedById",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Registration_RevokedById",
                table: "Registration");

            migrationBuilder.AlterColumn<long>(
                name: "RevokedById",
                table: "Registration",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedById",
                table: "Registration",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Registration_ApprovedById",
                table: "Registration",
                column: "ApprovedById",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Registration_RevokedById",
                table: "Registration",
                column: "RevokedById",
                principalTable: "Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
