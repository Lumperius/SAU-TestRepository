using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodMoodProvider.Migrations
{
    public partial class ModelCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Role",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Role");
        }
    }
}
