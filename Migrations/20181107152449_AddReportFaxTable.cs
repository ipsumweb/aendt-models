using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class AddReportFaxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                table: "ClinicSites",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "CameraCode",
                table: "ClinicSites",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "FaxAttempts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiagnosticReportID = table.Column<int>(nullable: false),
                    InterfaxID = table.Column<long>(nullable: false),
                    PagesSent = table.Column<int>(nullable: false),
                    AttemptsMade = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaxAttempts", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaxAttempts");

            migrationBuilder.AlterColumn<int>(
                name: "Fax",
                table: "ClinicSites",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "CameraCode",
                table: "ClinicSites",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
