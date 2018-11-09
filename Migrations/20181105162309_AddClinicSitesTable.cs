using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class AddClinicSitesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
//                table: "DiagnosticReports");
//
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
//                table: "DiagnosticReports");
//
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
//                table: "DiagnosticReports");

            migrationBuilder.AlterColumn<int>(
                name: "ReferralTimeframeID",
                table: "DiagnosticReports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReferralEntityID",
                table: "DiagnosticReports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagementPlanID",
                table: "DiagnosticReports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClinicSites",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CameraID = table.Column<int>(nullable: false),
                    CameraCode = table.Column<string>(maxLength: 50, nullable: false),
                    LocationName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Fax = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSites", x => x.ID);
                });

//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
//                table: "DiagnosticReports",
//                column: "ManagementPlanID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Cascade);
//
//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
//                table: "DiagnosticReports",
//                column: "ReferralEntityID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Cascade);
//
//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
//                table: "DiagnosticReports",
//                column: "ReferralTimeframeID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
//                table: "DiagnosticReports");
//
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
//                table: "DiagnosticReports");
//
//            migrationBuilder.DropForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
//                table: "DiagnosticReports");

            migrationBuilder.DropTable(
                name: "ClinicSites");

            migrationBuilder.AlterColumn<int>(
                name: "ReferralTimeframeID",
                table: "DiagnosticReports",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReferralEntityID",
                table: "DiagnosticReports",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ManagementPlanID",
                table: "DiagnosticReports",
                nullable: true,
                oldClrType: typeof(int));

//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
//                table: "DiagnosticReports",
//                column: "ManagementPlanID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Restrict);
//
//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
//                table: "DiagnosticReports",
//                column: "ReferralEntityID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Restrict);
//
//            migrationBuilder.AddForeignKey(
//                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
//                table: "DiagnosticReports",
//                column: "ReferralTimeframeID",
//                principalTable: "RecommendationPlans",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Restrict);
        }
    }
}
