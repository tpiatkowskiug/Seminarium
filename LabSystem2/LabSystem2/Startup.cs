using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(LabSystem2.Startup))]
namespace LabSystem2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        //        public void Configuration(IAppBuilder app)
        //        {
        //            ConfigureAuth(app);
        //#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
        //            app.UseHangfire(config =>
        //            {
        //                config.UseAuthorizationFilters(new AuthorizationFilter
        //                {
        //                    Roles = "Admin"
        //                });

        //                config.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //                config.UseServer();
        //            });
        //#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
        //        }
    }
}
