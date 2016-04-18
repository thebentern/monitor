using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    public sealed class RedisMessageSubscriber : RedisSubscription, ISubscribeToMessages
    {
        public RedisMessageSubscriber( string host, string channel ) : base( host, channel ) { }

        public void Subscribe( Action<IMessage> callback )
        {
            subscriber.Subscribe( redisChannel, CreateMessageHandler( callback ) );
        }

        public async Task SubscribeAsync( Action<IMessage> callback )
        {
            await subscriber.SubscribeAsync( redisChannel, CreateMessageHandler( callback ) );
        }

        private Action<RedisChannel, RedisValue> CreateMessageHandler( Action<IMessage> action )
        {
            return new Action<RedisChannel, RedisValue>( ( c, m ) =>
                action.Invoke( JsonConvert.DeserializeObject<IMessage>( m ) )
            );
        }
    }
}
