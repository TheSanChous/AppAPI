using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addgroupidentifier3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_Identifier",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Identifier",
                table: "Groups",
                column: "Identifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_Identifier",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Identifier",
                table: "Groups",
                column: "Identifier",
                unique: true,
                filter: "[Identifier] IS NOT NULL");
        }
    }
}
