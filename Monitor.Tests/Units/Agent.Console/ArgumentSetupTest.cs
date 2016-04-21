using System;
using NUnit.Framework;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using Fclp;
using FluentAssertions;
using Monitor.Agent.Console;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ArgumentSetupTest
    {
        readonly FluentCommandLineParser<ConsoleAgentArgs> parser = ArgumentSetup.Init();
        [Test]
        public void StdOut_Argument_Defaults_To_True()
        {
            var args = new string[0];
            var result = parser.Parse(args);
            result.HasErrors.Should().BeFalse();
            var agentArgs = parser.Object;
            agentArgs.StdOut.Should().BeTrue();
            agentArgs.Process.Should().BeNullOrEmpty();
        }
    }
}
