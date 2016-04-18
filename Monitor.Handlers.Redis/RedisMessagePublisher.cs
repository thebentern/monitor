using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    public class RedisMessagePublisher : RedisSubscription, IPublishMessages
    {
        public string originName = "Default";

        public RedisMessagePublisher( string host, string channel, string origin = null ) : base(host, channel)
        {
            if (origin != null)
                originName = origin;
        }

        public string Origin => originName;

        public long Publish( IMessage message )
        {
            return subscriber.Publish( redisChannel, JsonConvert.SerializeObject(message) );
        }

        public async Task<long> PublishAsync( IMessage message )
        {
            return await subscriber.PublishAsync( redisChannel, JsonConvert.SerializeObject(message) );
        }
    }
}
