using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Accent.Web.Startup))]
namespace Accent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
