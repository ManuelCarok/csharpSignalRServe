using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace SignalRTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {

            app.UseCors(CorsOptions.AllowAll);
            var config = new HubConfiguration();
            config.EnableJSONP = true;

            app.MapSignalR(config);
        }
    }
}