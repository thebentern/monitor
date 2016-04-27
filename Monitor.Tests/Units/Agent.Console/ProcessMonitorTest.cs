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
        public Mock<IProcess> process;
        [SetUp]
        public void Init_Process()
        {
            process = new Mock<IProcess>();
        }

        [Test]
        public void Throw_Exception_When_Publisher_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new ProcessMonitor(null, process.Object));

        [Test]
        public void Throw_Exception_When_process_Is_Missing() =>
            Assert.Throws<ArgumentNullException>(() => new ProcessMonitor(redisPublisher, null));

        [Test]
        public void Publishes_Message_From_Process()
        {
            var monitor = new ProcessMonitor(redisPublisher, process.Object);
            monitor.Monitor();
            process.Verify(p => p.Execute(
                    It.IsAny<EventHandler<MessageEventArgs>>()
                )
            );
            redisSubscriber.Subscribe(CheckMessage);
            process.Raise(p => p.RaiseMessageReceived += null, new MessageEventArgs(
                new DefaultMessage()
                {
                    Content = "Derp"
                })
            );
        }

        [Test]
        public void Publishes_Message_Async_From_Process()
        {
            var monitor = new ProcessMonitor(redisPublisher, process.Object);
            monitor.MonitorAsync();
            process.Verify(p => p.Execute(
                    It.IsAny<EventHandler<MessageEventArgs>>()
                )
            );
            redisSubscriber.Subscribe(CheckMessage);
            process.Raise(p => p.RaiseMessageReceived += null, new MessageEventArgs(
                new DefaultMessage()
                {
                    Content = "Derp"
                })
            );
        }

        private void CheckMessage(IMessage message)
        {
            message.Content.Should().Be("Derp");
        }
    }
}
