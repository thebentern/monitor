using Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public interface IMonitor<T> where T : IMessage
    {
        void Monitor(IPublishMessages<T> messagePublisher) ;
        void MonitorAsync(IPublishMessages<T> messagePublisher);
    }
}
