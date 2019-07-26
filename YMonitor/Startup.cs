using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YMonitor.Startup))]
namespace YMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}
