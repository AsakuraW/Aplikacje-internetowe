using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibApp.Startup))]
namespace LibApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
