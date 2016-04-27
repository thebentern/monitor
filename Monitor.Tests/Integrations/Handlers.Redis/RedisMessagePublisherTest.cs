using System;
using NUnit.Framework;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;

namespace Monitor.Tests.Integrations.Redis
{
    public class RedisMessagePublisherTest : RedisBaseTest
    {
        [Test]
        public void Subscriber_Recieves_Message_From_Redis_Instance()
        {
            long result = redisPublisher.Publish(newMessage);
            result.Should().BeGreaterThan(-1);
        }

        [Test]
        public async Task Subscriber_Recieves_Asynchronous_Message_From_Redis_Instance()
        {
            long result = await redisPublisher.PublishAsync(newMessage);
            result.Should().BeGreaterThan(-1);
        }

        [Test]
        public void Subscriber_Observes_The_Channel_And_Origin()
        {
            var channel = "MyChannel";
            var origin = "MyOrigin";
            var publisher = new RedisMessagePublisher<DefaultMessage>("localhost", channel, origin);
            publisher.Origin.Should().BeEquivalentTo(origin);
            publisher.Channel.Should().BeEquivalentTo(channel);
        }
    }
}
