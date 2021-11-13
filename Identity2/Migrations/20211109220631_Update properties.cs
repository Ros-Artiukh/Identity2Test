using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity2.Migrations
{
    public partial class Updateproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Developers_DeveloperId",
                table: "DeveloperProject");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Projects_ProjectId",
                table: "DeveloperProject");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "DeveloperProject",
                newName: "ProjectsId");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "DeveloperProject",
                newName: "DevelopersId");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperProject_ProjectId",
                table: "DeveloperProject",
                newName: "IX_DeveloperProject_ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Developers_DevelopersId",
                table: "DeveloperProject",
                column: "DevelopersId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Projects_ProjectsId",
                table: "DeveloperProject",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Developers_DevelopersId",
                table: "DeveloperProject");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperProject_Projects_ProjectsId",
                table: "DeveloperProject");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "DeveloperProject",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "DevelopersId",
                table: "DeveloperProject",
                newName: "DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperProject_ProjectsId",
                table: "DeveloperProject",
                newName: "IX_DeveloperProject_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Developers_DeveloperId",
                table: "DeveloperProject",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperProject_Projects_ProjectId",
                table: "DeveloperProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
