using Microsoft.EntityFrameworkCore.Migrations;

namespace Solucao.Application.Migrations
{
    public partial class addednewcolumnsandchangedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RgIe",
                table: "Clients",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "CpfCnpj",
                table: "Clients",
                newName: "Cnpj");

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "Clients",
                type: "varchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameForReceipt",
                table: "Clients",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Clients",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ie",
                table: "Clients",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsReceipt1",
                table: "Clients",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Ie",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsReceipt1",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "Clients",
                newName: "RgIe");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "Clients",
                newName: "CpfCnpj");

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "Clients",
                type: "varchar(50)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameForReceipt",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
