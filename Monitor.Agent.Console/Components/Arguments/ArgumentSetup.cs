using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    public static class ArgumentSetup
    {
        public static FluentCommandLineParser<ConsoleAgentArgs> Init()
        {
            var p = new FluentCommandLineParser<ConsoleAgentArgs>();
            // specify which property the value will be assigned too.
            p.Setup(arg => arg.Channel)
            .As('c', "channel")
            .WithDescription("Channel to publish messages to")
            .SetDefault("Default");

            p.Setup(arg => arg.RedisHost)
            .As('r', "redishost")
            .WithDescription("Redis server IP or name")
            .SetDefault("localhost");

            p.Setup(arg => arg.Origin)
            .As('o', "origin")
            .WithDescription("Origin (server or instance) of messages being published")
            .SetDefault("Default");

            p.Setup(arg => arg.Process)
            .As('p', "process")
            .WithDescription("Process (and arguments) to host in the agent")
            .SetDefault( default(String) );

            p.Setup(arg => arg.StdOut)
            .As('s', "stdout")
            .WithDescription("Capture StdOut stream piped into agent")
            .SetDefault(true);

            return p;
        }
   }
}
