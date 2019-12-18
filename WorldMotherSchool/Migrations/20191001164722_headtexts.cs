using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class headtexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Head",
                table: "EventAbouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Head",
                table: "EventAbouts",
                nullable: false,
                defaultValue: "");
        }
    }
}
