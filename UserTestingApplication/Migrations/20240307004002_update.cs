using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserTestingApplication.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTest_AspNetUsers_ApplicationUserId",
                table: "CompletedTest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompletedTest");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "CompletedTest",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTest_AspNetUsers_ApplicationUserId",
                table: "CompletedTest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTest_AspNetUsers_ApplicationUserId",
                table: "CompletedTest");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "CompletedTest",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CompletedTest",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTest_AspNetUsers_ApplicationUserId",
                table: "CompletedTest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
