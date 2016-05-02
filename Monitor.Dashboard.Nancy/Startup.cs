using Owin;
using Microsoft.AspNet.SignalR;

namespace Monitor.Dashboard.Nancy
{
    /// <summary>
    /// Bootstrapper for the web application startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration of the the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            app
                .MapSignalR("/signalr", new HubConfiguration()
                {
                    EnableJavaScriptProxies = true
                })
                .UseNancy();
        }
    }
}
