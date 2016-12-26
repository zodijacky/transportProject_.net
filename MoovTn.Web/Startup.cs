using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoovTn.Web.Startup))]
namespace MoovTn.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
