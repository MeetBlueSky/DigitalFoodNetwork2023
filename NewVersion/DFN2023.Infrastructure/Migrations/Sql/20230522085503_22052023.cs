using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _22052023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromUser",
                schema: "dbo",
                table: "Message",
                column: "FromUser");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Message",
                schema: "dbo",
                table: "Message",
                column: "FromUser",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

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
    }
}
