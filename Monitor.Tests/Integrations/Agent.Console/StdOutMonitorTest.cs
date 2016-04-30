using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Monitor.Agent.Console;
using Monitor.Core;
using Monitor.Tests;
using NUnit.Framework;

namespace Monitor.Tests.Units.Agent.Console
{
    public class StdOutMonitorTest : RedisBaseTest
    {
        private MemoryStream streamIn;
        private MemoryStream streamOut;

        private readonly string testMessage = TestHelpers.Repeat("derp", 100);

        [SetUp]
        public void Init_Stream()
        {
            streamIn = new MemoryStream();
            var bytes = Encoding.Default.GetBytes(testMessage);
            streamIn.Write(bytes, 0, bytes.Length);
            streamIn.Flush();
            streamIn.Position = 0;
            streamOut = new MemoryStream();
        }
        [TearDown]
        public void Close_Stream()
        {
            streamIn?.Close();
            streamOut?.Close();
        }

        [Test]
        public void Throw_Exception_When_Publisher_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new StdOutMonitor(null, streamIn, streamOut));

        [Test]
        public void Throw_Exception_When_StreamIn_Is_Missing() => 
            Assert.Throws<ArgumentNullException>(() => new StdOutMonitor(redisPublisher, null, streamOut));

        [Test]
        public void Throw_Exception_When_StreamOut_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new StdOutMonitor(redisPublisher, streamIn, null));

        [Test]
        public void Publishes_Message_From_Stream()
        {  
            redisSubscriber.Subscribe(CheckMessage);
            var monitor = new StdOutMonitor(redisPublisher, streamIn, streamOut);
            monitor.Monitor();
        }

        [Test]
        public async Task Publishes_Async_Message_From_Stream()
        {
            await redisSubscriber.SubscribeAsync(CheckMessage);
            var monitor = new StdOutMonitor(redisPublisher, streamIn, streamOut);
            monitor.MonitorAsync();
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().Contain("derp");
        }
    }
}
