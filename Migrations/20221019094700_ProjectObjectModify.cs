using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLMK.Migrations
{
    public partial class ProjectObjectModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkedDocumentCountPerHierarchy",
                table: "objects",
                newName: "LinkedDocuments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkedDocuments",
                table: "objects",
                newName: "LinkedDocumentCountPerHierarchy");
        }
    }
}
