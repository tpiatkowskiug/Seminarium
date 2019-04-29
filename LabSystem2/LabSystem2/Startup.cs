using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabSystem2.Startup))]
namespace LabSystem2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
