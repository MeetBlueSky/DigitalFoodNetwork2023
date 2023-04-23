using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _200420231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToUser",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FromUser",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "ToUser",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromUser",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
