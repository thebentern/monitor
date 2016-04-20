using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Handlers.Redis
{
    public class RedisSubscription : IDisposable
    {
        private readonly ConnectionMultiplexer redis;

        protected ISubscriber subscriber;
        protected RedisChannel redisChannel;

        public RedisSubscription( string host, string channel )
        {
            Channel = channel;
            redis = ConnectionMultiplexer.Connect( host );
            redisChannel = new RedisChannel( channel, RedisChannel.PatternMode.Literal );
            subscriber = redis.GetSubscriber();
        }

        public string Channel { get; }

        public void Dispose()
        {
            redis?.Close();
        }
    }
}
