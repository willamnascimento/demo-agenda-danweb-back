using Microsoft.EntityFrameworkCore.Migrations;

namespace Solucao.Application.Migrations
{
    public partial class add_new_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicCellPhone",
                table: "Clients",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "Clients",
                type: "varchar(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Has220V",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAirConditioning",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasStairs",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTechnique",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnnualContract",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhysicalPerson",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceipt",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LandMark",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameForReceipt",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RgIe",
                table: "Clients",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Secretary",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TakeTransformer",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TechniqueOption1",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechniqueOption2",
                table: "Clients",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Clients",
                type: "varchar(9)",
                maxLength: 9,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicCellPhone",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Has220V",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HasAirConditioning",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HasStairs",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HasTechnique",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsAnnualContract",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsPhysicalPerson",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsReceipt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LandMark",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NameForReceipt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RgIe",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Secretary",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TakeTransformer",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TechniqueOption1",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TechniqueOption2",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Clients");
        }
    }
}
