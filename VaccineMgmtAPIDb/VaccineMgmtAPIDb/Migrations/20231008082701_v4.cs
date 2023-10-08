using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccineMgmtAPIDb.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineStock_VaccineCenter_CenterId",
                table: "VaccineStock");

            migrationBuilder.DropTable(
                name: "VaccineModule");

            migrationBuilder.DropIndex(
                name: "IX_VaccineStock_CenterId",
                table: "VaccineStock");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "VaccineStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "VaccineStock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VaccineModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    CentreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    VaccineName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineModule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineStock_CenterId",
                table: "VaccineStock",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineStock_VaccineCenter_CenterId",
                table: "VaccineStock",
                column: "CenterId",
                principalTable: "VaccineCenter",
                principalColumn: "CenterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
