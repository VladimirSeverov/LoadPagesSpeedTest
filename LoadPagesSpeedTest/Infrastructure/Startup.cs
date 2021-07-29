using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LoadPagesSpeedTest.Infrastructure.Startup))]

namespace LoadPagesSpeedTest.Infrastructure
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
