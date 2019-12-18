using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class slideImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SlideFigures",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SlideFigures");
        }
    }
}
