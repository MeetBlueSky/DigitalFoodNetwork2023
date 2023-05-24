using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _220520231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message",
                column: "ToUser");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Message_To",
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
                name: "FK_User_Message_To",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ToUser",
                schema: "dbo",
                table: "Message");
        }
    }
}
