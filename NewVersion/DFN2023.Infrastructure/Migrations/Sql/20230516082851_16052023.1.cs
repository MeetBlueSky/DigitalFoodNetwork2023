using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _160520231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                schema: "dbo",
                table: "ProductCompany",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                schema: "dbo",
                table: "ProductCompany");
        }
    }
}
