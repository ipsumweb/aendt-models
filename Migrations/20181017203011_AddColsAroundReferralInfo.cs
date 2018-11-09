using Microsoft.EntityFrameworkCore.Migrations;

namespace AENDiagnosticTracker.Migrations
{
    public partial class AddColsAroundReferralInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "ManagementPlanID",
                table: "DiagnosticReports",
                nullable: true);

           

            migrationBuilder.AddColumn<int>(
                name: "ReferralEntityID",
                table: "DiagnosticReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferralTimeframeID",
                table: "DiagnosticReports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticReports_ManagementPlanID",
                table: "DiagnosticReports",
                column: "ManagementPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticReports_ReferralEntityID",
                table: "DiagnosticReports",
                column: "ReferralEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticReports_ReferralTimeframeID",
                table: "DiagnosticReports",
                column: "ReferralTimeframeID");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
                table: "DiagnosticReports",
                column: "ManagementPlanID",
                principalTable: "RecommendationPlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
                table: "DiagnosticReports",
                column: "ReferralEntityID",
                principalTable: "RecommendationPlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
                table: "DiagnosticReports",
                column: "ReferralTimeframeID",
                principalTable: "RecommendationPlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
               "update DiagnosticReports set ManagementPlanID = 1, ReferralEntityID = 11, ReferralTimeframeID = 7"
               );


            migrationBuilder.DropColumn(
                name: "ManagementPlan",
                table: "DiagnosticReports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ManagementPlanID",
                table: "DiagnosticReports");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ReferralEntityID",
                table: "DiagnosticReports");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID",
                table: "DiagnosticReports");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticReports_ManagementPlanID",
                table: "DiagnosticReports");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticReports_ReferralEntityID",
                table: "DiagnosticReports");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticReports_ReferralTimeframeID",
                table: "DiagnosticReports");

            migrationBuilder.DropColumn(
                name: "ManagementPlanID",
                table: "DiagnosticReports");

            migrationBuilder.DropColumn(
                name: "ReferralEntityID",
                table: "DiagnosticReports");

            migrationBuilder.DropColumn(
                name: "ReferralTimeframeID",
                table: "DiagnosticReports");

            migrationBuilder.AddColumn<string>(
                name: "ManagementPlan",
                table: "DiagnosticReports",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");
        }
    }
}
