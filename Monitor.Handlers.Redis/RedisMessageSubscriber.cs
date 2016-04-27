using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    public sealed class RedisMessageSubscriber<T> : RedisSubscription, ISubscribeToMessages<T> where T : IMessage
    {
        public RedisMessageSubscriber( string host, string channel ) : base( host, channel ) { }

        public void Subscribe( Action<T> callback )
        {
            subscriber.Subscribe( redisChannel, CreateMessageHandler( callback ) );
        }

        public async Task SubscribeAsync( Action<T> callback )
        {
            await subscriber.SubscribeAsync( redisChannel, CreateMessageHandler( callback ) );
        }

        private Action<RedisChannel, RedisValue> CreateMessageHandler( Action<T> action )
        {
            return
                (c, m) => action.Invoke(JsonConvert.DeserializeObject<T>(m));
        }
    }
}
