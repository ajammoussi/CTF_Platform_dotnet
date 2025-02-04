using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTF_Platform_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class MakeTeamIdNullable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CreatedByUserId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Teams",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CreatedByUserId",
                table: "Teams",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CreatedByUserId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CreatedByUserId",
                table: "Teams",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
