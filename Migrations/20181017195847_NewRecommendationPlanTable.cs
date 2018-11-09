using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class NewRecommendationPlanTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "update DiagnosticReports set PatientName='', Location = '', DRS = '', Gender = ''");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "DiagnosticReports",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManagementPlan",
                table: "DiagnosticReports",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "DiagnosticReports",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 75,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DiagnosticReports",
                type: "nchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DRS",
                table: "DiagnosticReports",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "DiagnosticReports",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ManagementPlan",
                table: "DiagnosticReports",
                type: "nvarchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "DiagnosticReports",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DiagnosticReports",
                type: "nchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1)");

            migrationBuilder.AlterColumn<string>(
                name: "DRS",
                table: "DiagnosticReports",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
