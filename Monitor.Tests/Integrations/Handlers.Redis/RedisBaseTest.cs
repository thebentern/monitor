using Monitor.Core;
using Monitor.Handlers.Redis;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Tests.Integrations.Redis
{
    public class RedisBaseTest
    {
        protected string Channel = "Default";
        protected string Host = "localhost";
        protected IMessage newMessage = new DefaultMessage()
        {
            Content = "Derp"
        };

        protected RedisMessagePublisher redisPublisher;
        protected RedisMessageSubscriber redisSubscriber;

        [SetUp]
        public void Init()
        {
            redisPublisher = new RedisMessagePublisher(Host, Channel);
            redisSubscriber = new RedisMessageSubscriber(Host, Channel);
        }

        [TearDown]
        public void Teardown()
        {
            redisPublisher.Dispose();
            redisSubscriber.Dispose();
        }
    }
}
