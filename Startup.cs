using AENDiagnosticTracker.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;
using AENDiagnosticTracker.Models;

namespace AENDiagnosticTracker
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // set up MVC services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                                .AddJsonOptions(options =>
                                {
                                    options.SerializerSettings.ContractResolver
                                        = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                                });

            // establish where the Angular app lives
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            var envSQL = Environment.GetEnvironmentVariable("ASPNETCORE_SQL_SERVER");
            if (envSQL != null)
                sqlConnectionStringBuilder.DataSource = envSQL;

            services.AddDbContext<AENDiagnosticContext>(options => options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
//                    spa.UseProxyToSpaDevelopmentServer("http://localhost:10000");
                }
            });
        }
    }
}
