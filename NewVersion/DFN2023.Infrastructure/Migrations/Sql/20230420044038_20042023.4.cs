using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _200420234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_FromUser",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ToUser",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_FromUser",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Message_FromUser",
                schema: "dbo",
                table: "Message",
                column: "FromUser");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message",
                column: "ToUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_FromUser",
                schema: "dbo",
                table: "Message",
                column: "FromUser",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ToUser",
                schema: "dbo",
                table: "Message",
                column: "ToUser",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
