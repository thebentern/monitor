using Monitor.Core;
using Monitor.Handlers.Redis;
using System;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Monitor Console Agent
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for Console Agent
        /// </summary>
        /// <param name="args">The application arguments.</param>
        /// <returns>
        /// 0 for successful execution
        /// 1 for argument errors
        /// </returns>
        public static int Main(string[] args)
        {
            var parser = ArgumentSetup.Init();
            var result = parser.Parse(args);
            var agentArgs = parser.Object;

            if (result.HasErrors)
            {
                System.Console.WriteLine(result.ErrorText);
                return 1;
            }

            IMonitor<DefaultMessage> process;
                
            IPublishMessages<DefaultMessage> publisher = 
                CreateRedisPublisher(agentArgs.Host, agentArgs.Channel, agentArgs.Origin);

            if (String.IsNullOrWhiteSpace(agentArgs.Process))
                process = new StdOutMonitor(publisher, System.Console.OpenStandardInput(), System.Console.OpenStandardOutput());
            else
                process = new ProcessMonitor(publisher, new Process(agentArgs.Process));

            process.Monitor();
            
            return 0;
        }

        /// <summary>
        /// Creates a redis publisher.
        /// </summary>
        /// <param name="host">The specified host.</param>
        /// <param name="channel">The specified channel.</param>
        /// <param name="origin">The specified origin.</param>
        /// <returns>
        /// An instance of IPublishMessages<see cref="DefaultMessage"/>
        /// </returns>
        private static IPublishMessages<DefaultMessage> CreateRedisPublisher(string host, string channel, string origin)
        {
            return new RedisMessagePublisher<DefaultMessage>(host, channel, origin);
        }
    }
}
