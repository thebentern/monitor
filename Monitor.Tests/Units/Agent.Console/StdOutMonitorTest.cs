using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Monitor.Agent.Console;
using Monitor.Core;
using Monitor.Tests.Integrations.Redis;
using NUnit.Framework;

namespace Monitor.Tests.Units.Agent.Console
{
    public class StdOutMonitorTest : RedisBaseTest
    {
        private MemoryStream streamIn;
        private MemoryStream streamOut;

        private StreamWriter writer;

        [SetUp]
        public void Init_Stream()
        {
            streamIn = new MemoryStream();
            streamOut = new MemoryStream();
            writer = new StreamWriter(streamIn);
        }
        [TearDown]
        public void Close_Stream()
        {
            streamIn?.Close();
            streamOut?.Close();
        }
        //TODO Make these tests actually capture published message and run asserts
        [Test]
        public async Task Publishes_Message_From_Stream()
        {
            var monitor = new StdOutMonitor(redisPublisher, streamIn, streamOut);
            monitor.Monitor();
            await redisSubscriber.SubscribeAsync(CheckMessage);
            writer.WriteLine("Derp");
        }

        [Test]
        public async Task Publishes_Async_Message_From_Stream()
        {
            var monitor = new StdOutMonitor(redisPublisher, streamIn, streamOut);
            monitor.MonitorAsync();
            await redisSubscriber.SubscribeAsync(CheckMessage);
            writer.WriteLine("Derp");
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().Be("Derp");
        }
    }
}
