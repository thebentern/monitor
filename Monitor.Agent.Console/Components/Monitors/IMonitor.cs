using Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public interface IMonitor
    {
        void Monitor(IPublishMessages messagePublisher);
        void MonitorAsync(IPublishMessages messagePublisher);

    }
}
