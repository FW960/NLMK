using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLMK.Migrations
{
    public partial class SmallFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedDocumentCount",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkedDocumentCount",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
