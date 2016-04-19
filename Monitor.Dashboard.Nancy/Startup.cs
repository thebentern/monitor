using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

namespace Monitor.Dashboard.Nancy
{
    public class Startup
    {
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
