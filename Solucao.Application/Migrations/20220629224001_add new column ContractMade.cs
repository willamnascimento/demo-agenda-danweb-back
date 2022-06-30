using Microsoft.EntityFrameworkCore.Migrations;

namespace Solucao.Application.Migrations
{
    public partial class addnewcolumnContractMade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContractMade",
                table: "Calendars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractMade",
                table: "Calendars");
        }
    }
}
