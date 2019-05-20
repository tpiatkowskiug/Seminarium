using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;

[assembly: OwinStartupAttribute(typeof(LabSystem2.Startup))]
namespace LabSystem2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
            app.UseHangfire(config =>
            {
                config.UseAuthorizationFilters(new AuthorizationFilter
                {
                    Roles = "Admin"
                });

                config.UseSqlServerStorage("DefaultConnection");
                config.UseServer();
            });
#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
        }
    }
}
