using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _200420235 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OfficialCountyId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfficialCountryId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfficialCityId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MapCountyId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MapCountryId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MapCityId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_MapCityId",
                schema: "dbo",
                table: "Company",
                column: "MapCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_MapCountryId",
                schema: "dbo",
                table: "Company",
                column: "MapCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_MapCountyId",
                schema: "dbo",
                table: "Company",
                column: "MapCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_OfficialCityId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_OfficialCountryId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_OfficialCountyId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_CompanyMap",
                schema: "dbo",
                table: "Company",
                column: "MapCityId",
                principalSchema: "dbo",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_City_CompanyOfficial",
                schema: "dbo",
                table: "Company",
                column: "OfficialCityId",
                principalSchema: "dbo",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_CompanyMap",
                schema: "dbo",
                table: "Company",
                column: "MapCountryId",
                principalSchema: "dbo",
                principalTable: "Country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_CompanyOfficial",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountryId",
                principalSchema: "dbo",
                principalTable: "Country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_County_CompanyMap",
                schema: "dbo",
                table: "Company",
                column: "MapCountyId",
                principalSchema: "dbo",
                principalTable: "County",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_County_CompanyOfficial",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountyId",
                principalSchema: "dbo",
                principalTable: "County",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_CompanyMap",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_City_CompanyOfficial",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_CompanyMap",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_CompanyOfficial",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_County_CompanyMap",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_County_CompanyOfficial",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_MapCityId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_MapCountryId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_MapCountyId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_OfficialCityId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_OfficialCountryId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_OfficialCountyId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "OfficialCountyId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfficialCountryId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OfficialCityId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MapCountyId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MapCountryId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MapCityId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
