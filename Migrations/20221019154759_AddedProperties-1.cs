using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLMK.Migrations
{
    public partial class AddedProperties1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkedDocumentsPerHierarchy",
                table: "objects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedDocumentsPerHierarchy",
                table: "objects");
        }
    }
}
