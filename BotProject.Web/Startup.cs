using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BotProject.Web.Startup))]
namespace BotProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
