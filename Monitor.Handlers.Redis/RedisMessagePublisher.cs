using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    public class RedisMessagePublisher<T> : RedisSubscription, IPublishMessages<T> where T : IMessage
    {
        public RedisMessagePublisher( string host, string channel, string origin = null ) : base(host, channel)
        {
            if (origin != null)
                Origin = origin;
        }

        public string Origin { get; } = "Default";

        public long Publish( T message )
        {
            return subscriber.Publish( redisChannel, JsonConvert.SerializeObject(message) );
        }

        public async Task<long> PublishAsync( T message )
        {
            return await subscriber.PublishAsync( redisChannel, JsonConvert.SerializeObject(message) );
        }
    }
}
