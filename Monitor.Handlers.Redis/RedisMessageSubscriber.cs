using System;
using System.Threading.Tasks;

using Monitor.Core;

using Newtonsoft.Json;

using StackExchange.Redis;

namespace Monitor.Handlers.Redis
{
    /// <summary>
    /// Redis implementation of the message subscriber
    /// </summary>
    /// <typeparam name="T">Message type</typeparam>
    public sealed class RedisMessageSubscriber<T> : RedisSubscription, ISubscribeToMessages<T> where T : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisMessageSubscriber{T}"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="channel">The channel.</param>
        public RedisMessageSubscriber(string host, string channel)
            : base(host, channel)
        {
            
        }

        /// <summary>
        /// Subscribes to Messages
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        public void Subscribe(Action<T> callback)
        {
            Subscriber.Subscribe(this.RedisChannel, CreateMessageHandler(callback));
        }

        /// <summary>
        /// Subscribes to Messages Asynchronously
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        /// <returns>Task of subscription</returns>
        public async Task SubscribeAsync(Action<T> callback)
        {
            await Subscriber.SubscribeAsync(this.RedisChannel, CreateMessageHandler(callback));
        }

        /// <summary>
        /// Creates a message handler for deserializing message.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Action of message handler</returns>
        private static Action<RedisChannel, RedisValue> CreateMessageHandler(Action<T> action)
        {
            return (c, m) => action.Invoke(JsonConvert.DeserializeObject<T>(m));
        }
    }
}
