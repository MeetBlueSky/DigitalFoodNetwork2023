using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _170520231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                schema: "dbo",
                table: "ProductCompany",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                schema: "dbo",
                table: "ProductCompany");
        }
    }
}
