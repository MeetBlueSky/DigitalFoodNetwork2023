using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _270420231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCompany_UserUrunler",
                schema: "dbo",
                table: "UserUrunler");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUrunler_Company_CompanyId",
                schema: "dbo",
                table: "UserUrunler");

            migrationBuilder.DropIndex(
                name: "IX_UserUrunler_Productd",
                schema: "dbo",
                table: "UserUrunler");

            migrationBuilder.DropColumn(
                name: "Productd",
                schema: "dbo",
                table: "UserUrunler");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_UserUrunler",
                schema: "dbo",
                table: "UserUrunler",
                column: "CompanyId",
                principalSchema: "dbo",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_UserUrunler",
                schema: "dbo",
                table: "UserUrunler");

            migrationBuilder.AddColumn<int>(
                name: "Productd",
                schema: "dbo",
                table: "UserUrunler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserUrunler_Productd",
                schema: "dbo",
                table: "UserUrunler",
                column: "Productd");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCompany_UserUrunler",
                schema: "dbo",
                table: "UserUrunler",
                column: "Productd",
                principalSchema: "dbo",
                principalTable: "ProductCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUrunler_Company_CompanyId",
                schema: "dbo",
                table: "UserUrunler",
                column: "CompanyId",
                principalSchema: "dbo",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
