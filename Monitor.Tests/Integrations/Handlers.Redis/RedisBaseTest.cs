using Monitor.Core;
using Monitor.Handlers.Redis;
using NUnit.Framework;
using System.Configuration;

namespace Monitor.Tests.Integrations.Redis
{
    /// <summary>
    /// Base class for Redis tests
    /// </summary>
    public class RedisBaseTest
    {
        protected string Channel = "Default";
        protected string Host = ConfigurationManager.AppSettings["RedisHost"] ?? "localhost";
        protected DefaultMessage newMessage = new DefaultMessage()
        {
            Content = "Derp"
        };

        protected RedisMessagePublisher<DefaultMessage> redisPublisher;
        protected RedisMessageSubscriber<DefaultMessage> redisSubscriber;

        [SetUp]
        public void Init()
        {
            redisPublisher = new RedisMessagePublisher<DefaultMessage>(Host, Channel);
            redisSubscriber = new RedisMessageSubscriber<DefaultMessage>(Host, Channel);
        }

        [TearDown]
        public void Teardown()
        {
            redisPublisher.Dispose();
            redisSubscriber.Dispose();
        }
    }
}
