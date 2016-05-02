using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Agent.Console
{
    /// <summary>
    /// Console Agent Argument Parsing
    /// </summary>
    public static class ArgumentSetup
    {
        /// <summary>
        /// Initializes and creates parser for Console Agent
        /// </summary>
        /// <returns>
        /// Command line arguments parser
        /// </returns>
        public static FluentCommandLineParser<ConsoleAgentArgs> Init()
        {
            var parser = new FluentCommandLineParser<ConsoleAgentArgs>();

            parser.Setup(arg => arg.Channel)
            .As('c', "channel")
            .WithDescription("Channel to publish messages to")
            .SetDefault("Default");

            parser.Setup(arg => arg.Host)
            .As('h', "host")
            .WithDescription("Address of host message consumer")
            .SetDefault("localhost");

            parser.Setup(arg => arg.Origin)
            .As('o', "origin")
            .WithDescription("Origin (server or instance) of messages being published")
            .SetDefault("Default");

            parser.Setup(arg => arg.Process)
            .As('p', "process")
            .WithDescription("Process (and arguments) to host in the agent")
            .SetDefault( default(String) );

            parser.Setup(arg => arg.StdOut)
            .As('s', "stdout")
            .WithDescription("Capture StdOut stream piped into agent")
            .SetDefault(true);

            return parser;
        }
   }
}
