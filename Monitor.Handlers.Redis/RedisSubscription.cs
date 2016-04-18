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
        private ConnectionMultiplexer redis;

        protected ISubscriber subscriber;
        protected RedisChannel redisChannel; 
        private readonly string channelName;

        public RedisSubscription( string host, string channel )
        {
            channelName = channel;
            redis = ConnectionMultiplexer.Connect( host );
            redisChannel = new RedisChannel( channel, RedisChannel.PatternMode.Literal );
            subscriber = redis.GetSubscriber();
        }

        public string Channel => channelName;

        public void Dispose()
        {
            redis?.Close();
        }
    }
}
