using System;
using NUnit.Framework;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;

namespace Monitor.Tests.Integrations.Redis
{
    public class RedisMessageSubscriberTest : RedisBaseTest
    {
        [Test]
        public void Publishes_Message_To_Redis_Instance()
        {
            redisSubscriber.Subscribe(CheckMessage);
            redisPublisher.Publish(newMessage).Should().BePositive();
        }

        [Test]
        public async Task Publishes_Message_Asynchronously_To_Redis_Instance()
        {
            await redisSubscriber.SubscribeAsync(CheckMessage);
            var result = await redisPublisher.PublishAsync(newMessage);
        }

        private void CheckMessage(IMessage message) 
            => message.Content.Should().Be("Derp");
    }
}
