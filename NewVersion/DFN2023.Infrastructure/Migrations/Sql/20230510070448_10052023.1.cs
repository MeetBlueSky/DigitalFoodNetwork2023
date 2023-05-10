using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _100520231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyShow",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "FromRolId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserShow",
                schema: "dbo",
                table: "Message",
                newName: "IsShow");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "Company",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message",
                column: "ToUser");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Message",
                schema: "dbo",
                table: "Message",
                column: "ToUser",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Message",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "IsShow",
                schema: "dbo",
                table: "Message",
                newName: "UserShow");

            migrationBuilder.AddColumn<bool>(
                name: "CompanyShow",
                schema: "dbo",
                table: "Message",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromRolId",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: true);
        }
    }
}
