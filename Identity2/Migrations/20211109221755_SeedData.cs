using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity2.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Billy" });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Van" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Client", "Name" },
                values: new object[] { 1, "Dungeon Master", "Gym" });

            migrationBuilder.InsertData(
                table: "DeveloperProject",
                columns: new[] { "DevelopersId", "ProjectsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "DeveloperProject",
                columns: new[] { "DevelopersId", "ProjectsId" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeveloperProject",
                keyColumns: new[] { "DevelopersId", "ProjectsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DeveloperProject",
                keyColumns: new[] { "DevelopersId", "ProjectsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
