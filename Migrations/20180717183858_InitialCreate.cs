using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICD10",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Summary = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: false),
                    Eye = table.Column<string>(type: "nchar(1)", nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD10", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    KYLicenseNumber = table.Column<string>(maxLength: 20, nullable: false),
                    LDAPID = table.Column<string>(maxLength: 20, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticReports",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientDemographicRaw = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(maxLength: 150, nullable: false),
                    Location = table.Column<string>(maxLength: 75, nullable: true),
                    DRS = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nchar(1)", nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    ImageCaptureDateTime = table.Column<DateTime>(nullable: false),
                    ManagementPlan = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DiagnosticReports_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportCodes",
                columns: table => new
                {
                    DiagnosticReportID = table.Column<int>(nullable: false),
                    ICD10ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCodes", x => new { x.DiagnosticReportID, x.ICD10ID });
                    table.ForeignKey(
                        name: "FK_ReportCodes_DiagnosticReports_DiagnosticReportID",
                        column: x => x.DiagnosticReportID,
                        principalTable: "DiagnosticReports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportCodes_ICD10_ICD10ID",
                        column: x => x.ICD10ID,
                        principalTable: "ICD10",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticReports_UserID",
                table: "DiagnosticReports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCodes_ICD10ID",
                table: "ReportCodes",
                column: "ICD10ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportCodes");

            migrationBuilder.DropTable(
                name: "DiagnosticReports");

            migrationBuilder.DropTable(
                name: "ICD10");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
