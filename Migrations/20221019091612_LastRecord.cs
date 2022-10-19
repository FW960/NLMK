using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLMK.Migrations
{
    public partial class LastRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalWorkingHours",
                table: "objects");

            migrationBuilder.RenameColumn(
                name: "WorkedInHours",
                table: "objects",
                newName: "LinkedDocumentCountPerHierarchy");

            migrationBuilder.AddColumn<int>(
                name: "LinkedDocumentCount",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedDocumentCount",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "LinkedDocumentCountPerHierarchy",
                table: "objects",
                newName: "WorkedInHours");

            migrationBuilder.AddColumn<int>(
                name: "TotalWorkingHours",
                table: "objects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
