using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solucao.Application.Migrations
{
    public partial class AddNewCollumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TemporaryName",
                table: "Calendars",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DriverCollectsId",
                table: "Calendars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_DriverCollectsId",
                table: "Calendars",
                column: "DriverCollectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_People_DriverCollectsId",
                table: "Calendars",
                column: "DriverCollectsId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_People_DriverCollectsId",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_DriverCollectsId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "DriverCollectsId",
                table: "Calendars");

            migrationBuilder.AlterColumn<string>(
                name: "TemporaryName",
                table: "Calendars",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);
        }
    }
}
