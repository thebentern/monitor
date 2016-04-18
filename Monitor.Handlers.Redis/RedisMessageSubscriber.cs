using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    public sealed class RedisMessageSubscriber : RedisSubscription, ISubscribeToMessages
    {
        public RedisMessageSubscriber( string host, string channel ) : base( host, channel ) { }

        public void Subscribe( Action<Message> callback )
        {
            subscriber.Subscribe( redisChannel, CreateMessageHandler( callback ) );
        }

        public async Task SubscribeAsync( Action<Message> callback )
        {
            await subscriber.SubscribeAsync( redisChannel, CreateMessageHandler( callback ) );
        }

        private Action<RedisChannel, RedisValue> CreateMessageHandler( Action<Message> action )
        {
            return new Action<RedisChannel, RedisValue>( ( c, m ) =>
                action.Invoke( JsonConvert.DeserializeObject<Message>( m ) )
            );
        }
    }
}
