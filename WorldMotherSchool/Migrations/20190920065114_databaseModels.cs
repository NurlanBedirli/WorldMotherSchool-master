using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class databaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "SlideFigures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResourcesViewId",
                table: "SlideFigures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "EventAbouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResourcesViewId",
                table: "EventAbouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesViews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    ResourcesViewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Associations_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AzSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    List = table.Column<string>(nullable: true),
                    Footer = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    ResourcesViewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AzSections_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AzSections_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorControls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    ResourcesViewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorControls_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorControls_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EngSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    List = table.Column<string>(nullable: true),
                    Footer = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngSections_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngSections_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthyFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthyFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthyFoods_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthyFoods_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGalareyas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Photo = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGalareyas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoGalareyas_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Psychologicals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psychologicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Psychologicals_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Psychologicals_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RuleAdmissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleAdmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleAdmissions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuleAdmissions_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RuSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    List = table.Column<string>(nullable: true),
                    Footer = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuSections_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuSections_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Head = table.Column<string>(nullable: true),
                    ResourcesViewId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Securities_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Securities_ResourcesViews_ResourcesViewId",
                        column: x => x.ResourcesViewId,
                        principalTable: "ResourcesViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlideFigures_LanguageId",
                table: "SlideFigures",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideFigures_ResourcesViewId",
                table: "SlideFigures",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAbouts_LanguageId",
                table: "EventAbouts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAbouts_ResourcesViewId",
                table: "EventAbouts",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_LanguageId",
                table: "Associations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ResourcesViewId",
                table: "Associations",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_AzSections_LanguageId",
                table: "AzSections",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AzSections_ResourcesViewId",
                table: "AzSections",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorControls_LanguageId",
                table: "DoctorControls",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorControls_ResourcesViewId",
                table: "DoctorControls",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_EngSections_LanguageId",
                table: "EngSections",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EngSections_ResourcesViewId",
                table: "EngSections",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_LanguageId",
                table: "Excursions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ResourcesViewId",
                table: "Excursions",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthyFoods_LanguageId",
                table: "HealthyFoods",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthyFoods_ResourcesViewId",
                table: "HealthyFoods",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGalareyas_ResourcesViewId",
                table: "PhotoGalareyas",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ResourcesViewId",
                table: "Photos",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Psychologicals_LanguageId",
                table: "Psychologicals",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Psychologicals_ResourcesViewId",
                table: "Psychologicals",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleAdmissions_LanguageId",
                table: "RuleAdmissions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleAdmissions_ResourcesViewId",
                table: "RuleAdmissions",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_RuSections_LanguageId",
                table: "RuSections",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RuSections_ResourcesViewId",
                table: "RuSections",
                column: "ResourcesViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_LanguageId",
                table: "Securities",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_ResourcesViewId",
                table: "Securities",
                column: "ResourcesViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAbouts_Languages_LanguageId",
                table: "EventAbouts",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAbouts_ResourcesViews_ResourcesViewId",
                table: "EventAbouts",
                column: "ResourcesViewId",
                principalTable: "ResourcesViews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlideFigures_Languages_LanguageId",
                table: "SlideFigures",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlideFigures_ResourcesViews_ResourcesViewId",
                table: "SlideFigures",
                column: "ResourcesViewId",
                principalTable: "ResourcesViews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAbouts_Languages_LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAbouts_ResourcesViews_ResourcesViewId",
                table: "EventAbouts");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideFigures_Languages_LanguageId",
                table: "SlideFigures");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideFigures_ResourcesViews_ResourcesViewId",
                table: "SlideFigures");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "AzSections");

            migrationBuilder.DropTable(
                name: "DoctorControls");

            migrationBuilder.DropTable(
                name: "EngSections");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "HealthyFoods");

            migrationBuilder.DropTable(
                name: "PhotoGalareyas");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Psychologicals");

            migrationBuilder.DropTable(
                name: "RuleAdmissions");

            migrationBuilder.DropTable(
                name: "RuSections");

            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "ResourcesViews");

            migrationBuilder.DropIndex(
                name: "IX_SlideFigures_LanguageId",
                table: "SlideFigures");

            migrationBuilder.DropIndex(
                name: "IX_SlideFigures_ResourcesViewId",
                table: "SlideFigures");

            migrationBuilder.DropIndex(
                name: "IX_EventAbouts_LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropIndex(
                name: "IX_EventAbouts_ResourcesViewId",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "SlideFigures");

            migrationBuilder.DropColumn(
                name: "ResourcesViewId",
                table: "SlideFigures");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "ResourcesViewId",
                table: "EventAbouts");
        }
    }
}
