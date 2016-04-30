using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Common;
using Monitor.Agent.Console;
using Monitor.Core;
using Monitor.Tests;
using NUnit.Framework;
using Process = Monitor.Agent.Console.Process;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ProgramTest : RedisBaseTest
    {
        private MemoryStream streamIn;

        private readonly string testMessage = TestHelpers.Repeat("derp", 100);

        [SetUp]
        public void Init_Stream()
        {
            streamIn = new MemoryStream();
            var bytes = Encoding.Default.GetBytes(testMessage);
            streamIn.Write(bytes, 0, bytes.Length);
            streamIn.Flush();
            streamIn.Position = 0;
        }
        [TearDown]
        public void Close_Stream()
        {
            streamIn?.Close();
        }

        [Test]
        public void Publishes_Message_From_Process()
        {
            redisSubscriber.Subscribe(CheckMessage);
            int result = Program.Main(new []{ "-p", "ipconfig" });
            result.Should().Be(0);
        }

        [Test]
        public void Returns_Error_Code_For_Invalid_Args()
        {
            int result = Program.Main(new[] { "-s", "bob" });
            result.Should().Be(1);
        }

        [Test]
        public void Publishes_Message_From_StdIn()
        {
            redisSubscriber.Subscribe(CheckMessage);
            var process = new Process(
                    Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Monitor.Agent.Console.exe")
                );
            process.Execute(() =>
            {
                process.StdIn.WriteLine(testMessage);
            });
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().Contain("derp");
        }
    }
}
