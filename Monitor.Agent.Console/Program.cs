﻿using Monitor.Core;
using Monitor.Handlers.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public class Program
    {
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

        private static IPublishMessages<DefaultMessage> CreateRedisPublisher(string host, string channel, string origin)
        {
            return new RedisMessagePublisher<DefaultMessage>(host, channel, origin);
        }
    }
}
