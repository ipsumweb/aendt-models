using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AENDiagnosticTracker.Models;

namespace AENDiagnosticTracker.Data
{
    public class AENDiagnosticContext : DbContext
    {
        public AENDiagnosticContext(DbContextOptions<AENDiagnosticContext> options) : base(options)
        {
        }

        public DbSet<DiagnosticReport> DiagnosticReports { get; set; }
        public DbSet<ICD10> ICD10s { get; set; }
        public DbSet<FaxAttempt> FaxAttempts { get; set; }
        public DbSet<ReportCode> ReportCodes { get; set; }
        public DbSet<RecommendationPlan> RecommendationPlans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClinicSite> ClinicSites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClinicSite>().ToTable("ClinicSites");
            modelBuilder.Entity<DiagnosticReport>().ToTable("DiagnosticReports");
            modelBuilder.Entity<FaxAttempt>().ToTable("FaxAttempts");
            modelBuilder.Entity<ICD10>().ToTable("ICD10");
            modelBuilder.Entity<RecommendationPlan>().ToTable("RecommendationPlans");
            modelBuilder.Entity<ReportCode>().ToTable("ReportCodes");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<ReportCode>().HasKey(c => new {c.DiagnosticReportID, c.ICD10ID});
        }
    }
}