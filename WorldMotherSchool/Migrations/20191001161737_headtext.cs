using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class headtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "EventAboutLanguages",
                newName: "TextLang");

            migrationBuilder.AddColumn<string>(
                name: "TextHead",
                table: "EventAboutLanguages",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextHead",
                table: "EventAboutLanguages");

            migrationBuilder.RenameColumn(
                name: "TextLang",
                table: "EventAboutLanguages",
                newName: "Text");
        }
    }
}
