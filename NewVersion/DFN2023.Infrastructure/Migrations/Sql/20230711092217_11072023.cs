using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _11072023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuManagement",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuFeatureId = table.Column<int>(type: "int", nullable: true),
                    ClickType = table.Column<int>(type: "int", nullable: false),
                    OpeningType = table.Column<int>(type: "int", nullable: true),
                    MenuLayer = table.Column<int>(type: "int", nullable: false),
                    MenuLayerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowNum = table.Column<int>(type: "int", nullable: false),
                    LangId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SliderHeader",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderHeader", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slider_Type",
                schema: "dbo",
                table: "Slider",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Slider_SliderHeader",
                schema: "dbo",
                table: "Slider",
                column: "Type",
                principalSchema: "dbo",
                principalTable: "SliderHeader",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slider_SliderHeader",
                schema: "dbo",
                table: "Slider");

            migrationBuilder.DropTable(
                name: "MenuManagement",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SliderHeader",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Slider_Type",
                schema: "dbo",
                table: "Slider");
        }
    }
}
