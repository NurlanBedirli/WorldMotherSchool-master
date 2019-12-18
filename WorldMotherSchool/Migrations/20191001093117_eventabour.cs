using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldMotherSchool.Migrations
{
    public partial class eventabour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAbouts_Languages_LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAbouts_ResourcesViews_ResourcesViewId",
                table: "EventAbouts");

            migrationBuilder.DropIndex(
                name: "IX_EventAbouts_LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropIndex(
                name: "IX_EventAbouts_ResourcesViewId",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "HeadCaption",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "EventAbouts");

            migrationBuilder.DropColumn(
                name: "ResourcesViewId",
                table: "EventAbouts");

            migrationBuilder.RenameColumn(
                name: "TextContent",
                table: "EventAbouts",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "EventAbouts",
                newName: "Head");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "SlideFigures",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "EventAbouts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "EventAbouts");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "EventAbouts",
                newName: "TextContent");

            migrationBuilder.RenameColumn(
                name: "Head",
                table: "EventAbouts",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "SlideFigures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadCaption",
                table: "EventAbouts",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "EventAbouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "EventAbouts",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ResourcesViewId",
                table: "EventAbouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventAbouts_LanguageId",
                table: "EventAbouts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAbouts_ResourcesViewId",
                table: "EventAbouts",
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
        }
    }
}
