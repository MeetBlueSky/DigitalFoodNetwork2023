using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _200420236 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Company_City_MapCityId",
                schema: "dbo",
                table: "Company",
                column: "MapCityId",
                principalSchema: "dbo",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_City_OfficialCityId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCityId",
                principalSchema: "dbo",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Country_MapCountryId",
                schema: "dbo",
                table: "Company",
                column: "MapCountryId",
                principalSchema: "dbo",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Country_OfficialCountryId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountryId",
                principalSchema: "dbo",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_County_MapCountyId",
                schema: "dbo",
                table: "Company",
                column: "MapCountyId",
                principalSchema: "dbo",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_County_OfficialCountyId",
                schema: "dbo",
                table: "Company",
                column: "OfficialCountyId",
                principalSchema: "dbo",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_City_MapCityId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_City_OfficialCityId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Country_MapCountryId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Country_OfficialCountryId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_County_MapCountyId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_County_OfficialCountyId",
                schema: "dbo",
                table: "Company");

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
    }
}
