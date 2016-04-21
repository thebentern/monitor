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
            var parser = ArgumentSetup.Init();
            var result = parser.Parse(args);
            var agentArgs = parser.Object;

            if ( result.HasErrors == false )
                System.Console.WriteLine( result.ErrorText );
            else
            {
                IMonitor<DefaultMessage> process;
                if ( String.IsNullOrWhiteSpace(agentArgs.Process) )
                    process = new StdOutMonitor();
                else
                    process = new ProcessMonitor( agentArgs.Process );

                IPublishMessages<DefaultMessage> publisher = CreateRedisPublisher( agentArgs.RedisHost, 
                    agentArgs.Channel, 
                    agentArgs.Origin );

                process.Monitor(publisher);
            }
        }

        public static IPublishMessages<DefaultMessage> CreateRedisPublisher(string host, string channel, string origin)
        {
            var publisher = new RedisMessagePublisher<DefaultMessage>(host, channel, origin);
            return publisher;
        }
    }
}
