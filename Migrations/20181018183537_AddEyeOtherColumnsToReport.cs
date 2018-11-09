using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class AddEyeOtherColumnsToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "LeftEyeOther",
                table: "DiagnosticReports",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightEyeOther",
                table: "DiagnosticReports",
                type: "nvarchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftEyeOther",
                table: "DiagnosticReports");

            migrationBuilder.DropColumn(
                name: "RightEyeOther",
                table: "DiagnosticReports");
            
        }
    }
}
