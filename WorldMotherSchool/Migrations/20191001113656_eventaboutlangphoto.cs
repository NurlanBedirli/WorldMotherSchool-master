using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class eventaboutlangphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "EventAbouts");

            migrationBuilder.CreateTable(
                name: "EventAboutLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    EventAboutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAboutLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventAboutLanguages_EventAbouts_EventAboutId",
                        column: x => x.EventAboutId,
                        principalTable: "EventAbouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAboutLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAboutPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Photo = table.Column<string>(nullable: false),
                    EventAboutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAboutPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventAboutPhotos_EventAbouts_EventAboutId",
                        column: x => x.EventAboutId,
                        principalTable: "EventAbouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventAboutLanguages_EventAboutId",
                table: "EventAboutLanguages",
                column: "EventAboutId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAboutLanguages_LanguageId",
                table: "EventAboutLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAboutPhotos_EventAboutId",
                table: "EventAboutPhotos",
                column: "EventAboutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAboutLanguages");

            migrationBuilder.DropTable(
                name: "EventAboutPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "EventAbouts",
                nullable: false,
                defaultValue: "");
        }
    }
}
