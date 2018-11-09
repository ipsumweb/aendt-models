using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class RemovingRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "DiagnosticReports",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ImageCaptureDateTime",
                table: "DiagnosticReports",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DiagnosticReports",
                type: "nchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "DiagnosticReports",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "PatientCode",
                table: "DiagnosticReports",
                type: "nvarchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientCode",
                table: "DiagnosticReports");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "DiagnosticReports",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ImageCaptureDateTime",
                table: "DiagnosticReports",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DiagnosticReports",
                type: "nchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "DiagnosticReports",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
