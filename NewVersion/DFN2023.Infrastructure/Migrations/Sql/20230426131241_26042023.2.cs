using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFN2023.Infrastructure.Migrations.Sql
{
    public partial class _260420232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Message",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ParentId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "MessageType",
                schema: "dbo",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                schema: "dbo",
                table: "Message",
                newName: "FromRolId");

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

            migrationBuilder.AddColumn<bool>(
                name: "CompanyShow",
                schema: "dbo",
                table: "Message",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UserShow",
                schema: "dbo",
                table: "Message",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyShow",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserShow",
                schema: "dbo",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "FromRolId",
                schema: "dbo",
                table: "Message",
                newName: "ParentId");

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

            migrationBuilder.AddColumn<string>(
                name: "MessageType",
                schema: "dbo",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ParentId",
                schema: "dbo",
                table: "Message",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Message",
                schema: "dbo",
                table: "Message",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "Message",
                principalColumn: "Id");
        }
    }
}
