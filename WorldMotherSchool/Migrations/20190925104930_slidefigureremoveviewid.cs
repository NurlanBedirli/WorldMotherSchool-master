using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class slidefigureremoveviewid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideFigures_ResourcesViews_ResourcesViewId",
                table: "SlideFigures");

            migrationBuilder.DropIndex(
                name: "IX_SlideFigures_ResourcesViewId",
                table: "SlideFigures");

            migrationBuilder.DropColumn(
                name: "ResourcesViewId",
                table: "SlideFigures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourcesViewId",
                table: "SlideFigures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SlideFigures_ResourcesViewId",
                table: "SlideFigures",
                column: "ResourcesViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideFigures_ResourcesViews_ResourcesViewId",
                table: "SlideFigures",
                column: "ResourcesViewId",
                principalTable: "ResourcesViews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
