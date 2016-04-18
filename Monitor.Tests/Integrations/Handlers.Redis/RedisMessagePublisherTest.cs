using System;
using NUnit.Framework;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Monitor.Tests.Integrations.Redis
{
    public class RedisMessagePublisherTest : RedisBaseTest
    {
        [Test]
        public void Subscriber_Recieves_Message_From_Redis_Instance()
        {
            long result = redisPublisher.Publish(newMessage);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Subscriber_Recieves_Asynchronous_Message_From_Redis_Instance()
        {
            long result = await redisPublisher.PublishAsync(newMessage);
            Assert.IsNotNull(result);
        }
    }
}
