using Microsoft.EntityFrameworkCore.Migrations;

namespace DietPlanner.Api.Migrations
{
    public partial class WeightPointChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "WeightPoints",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightPoints_UserId",
                table: "WeightPoints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightPoints_Users_UserId",
                table: "WeightPoints",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeightPoints_Users_UserId",
                table: "WeightPoints");

            migrationBuilder.DropIndex(
                name: "IX_WeightPoints_UserId",
                table: "WeightPoints");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WeightPoints",
                newName: "PlanId");
        }
    }
}
