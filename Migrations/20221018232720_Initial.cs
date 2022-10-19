using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLMK.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "objects",
                columns: table => new
                {
                    ObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedObjectId = table.Column<int>(type: "int", nullable: true),
                    RelatedProjectId = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    WorkingHoursStandard = table.Column<int>(type: "int", nullable: false),
                    WorkedInHours = table.Column<int>(type: "int", nullable: false),
                    TotalWorkingHours = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ProjectObjectObjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ObjectId", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_objects_objects_ProjectObjectObjectId",
                        column: x => x.ProjectObjectObjectId,
                        principalTable: "objects",
                        principalColumn: "ObjectId");
                    table.ForeignKey(
                        name: "FK_objects_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_objects_ProjectId",
                table: "objects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_objects_ProjectObjectObjectId",
                table: "objects",
                column: "ProjectObjectObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "objects");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
