using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserTestingApplication.Migrations
{
    /// <inheritdoc />
    public partial class updated_ApplicationUserTest_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Test");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ApplicationUserTest",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "ApplicationUserTest");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Test",
                type: "integer",
                nullable: true);
        }
    }
}
