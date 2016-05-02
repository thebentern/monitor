using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    /// <summary>
    /// Redis implementation of the message subscriber
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RedisMessageSubscriber<T> : RedisSubscription, ISubscribeToMessages<T> where T : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisMessageSubscriber{T}"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="channel">The channel.</param>
        public RedisMessageSubscriber(string host, string channel) : base( host, channel ) { }

        /// <summary>
        /// Subscribes to Messages
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        public void Subscribe(Action<T> callback)
        {
            Subscriber.Subscribe(RedisChannel, CreateMessageHandler(callback));
        }

        /// <summary>
        /// Subscribes to Messages Asynchronously
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        /// <returns></returns>
        public async Task SubscribeAsync(Action<T> callback)
        {
            await Subscriber.SubscribeAsync(RedisChannel, CreateMessageHandler(callback));
        }

        /// <summary>
        /// Creates a message handler.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private static Action<RedisChannel, RedisValue> CreateMessageHandler(Action<T> action)
        {
            return (c, m) => action.Invoke(JsonConvert.DeserializeObject<T>(m));
        }
    }
}
