using Monitor.Core;
using Monitor.Handlers.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IMonitor process;
            var parser = ArgumentSetup.Init();
            var result = parser.Parse(args);
            var agentArgs = parser.Object;

            if ( result.HasErrors == false )
                System.Console.WriteLine( result.ErrorText );
            else
            {
                if ( agentArgs.StdOut )
                    process = new StdOutMonitor();
                else
                    process = new ProcessMonitor( agentArgs.Process );

                IPublishMessages publisher = CreateRedisPublisher( agentArgs.RedisHost, 
                    agentArgs.Channel, 
                    agentArgs.Origin );

                process.Monitor(publisher);
            }
        }

        public static IPublishMessages CreateRedisPublisher(string host, string channel, string origin)
        {
            var publisher = new RedisMessagePublisher(host, channel, origin);
            return publisher;
        }
    }
}
