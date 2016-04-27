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
            redisPublisher.Publish(newMessage).Should().BeGreaterThan(-1);
        }

        [Test]
        public async Task Publishes_Message_Asynchronously_To_Redis_Instance()
        {
            await redisSubscriber.SubscribeAsync(CheckMessage);
            var result = await redisPublisher.PublishAsync(newMessage);
        }


        [Test]
        public void Publishes_Observes_The_Channel_And_Origin()
        {
            var channel = "MyChannel";
            var origin = "MyOrigin";
            var subscriber = new RedisMessageSubscriber<DefaultMessage>("localhost", channel);
            var publisher = new RedisMessagePublisher<DefaultMessage>("localhost", channel, origin);
            subscriber.Subscribe((m) => m.Should().ShouldBeEquivalentTo(channel));
            publisher.Publish(new DefaultMessage(channel) {Content = "Derp"});
        }

        private void CheckMessage(IMessage message) 
            => message.Content.Should().Be("Derp");
    }
}
