using System;
using FluentAssertions;
using Monitor.Agent.Console;
using Monitor.Core;
using NUnit.Framework;
using Moq;

namespace Monitor.Tests.Units.Agent.Console
{
    public class ProcessMonitorTest : RedisBaseTest
    {
        IProcess process;
        [SetUp]
        public void Init_Process()
        {
            process = new Process("ipconfig");
        }

        [Test]
        public void Throw_Exception_When_Publisher_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new ProcessMonitor(null, process));

        [Test]
        public void Throw_Exception_When_Process_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new ProcessMonitor(redisPublisher, null));

        [Test]
        public void Publishes_Message_From_Process()
        {
            var monitor = new ProcessMonitor(redisPublisher, process);
            redisSubscriber.Subscribe(CheckMessage);
            monitor.Monitor();
        }

        [Test]
        public void Publishes_Message_Async_From_Process()
        {
            var monitor = new ProcessMonitor(redisPublisher, process);
            redisSubscriber.Subscribe(CheckMessage);
            monitor.MonitorAsync();
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().NotBeNullOrEmpty();
        }
    }
}
