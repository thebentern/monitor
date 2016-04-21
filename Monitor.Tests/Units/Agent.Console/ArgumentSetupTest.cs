using NUnit.Framework;
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

        [Test]
        public void Host_Argument_Defaults_To_localhost()
        {
            var args = new string[0];
            var result = parser.Parse(args);
            result.HasErrors.Should().BeFalse();
            var agentArgs = parser.Object;
            agentArgs.Host.Should().Be("localhost");

        }
    }
}
