using Newtonsoft.Json;
using System.Threading.Tasks;
using Monitor.Core;

namespace Monitor.Handlers.Redis
{
    /// <summary>
    /// Redis implementation of message publisher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RedisMessagePublisher<T> : RedisSubscription, IPublishMessages<T> where T : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisMessagePublisher{T}"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="origin">The origin.</param>
        public RedisMessagePublisher(string host, string channel, string origin = null) : base(host, channel)
        {
            if (origin != null)
                Origin = origin;
        }

        /// <summary>
        /// Gets the message subscription Origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; } = "Default";

        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public long Publish(T message)
        {
            return Subscriber.Publish(RedisChannel, JsonConvert.SerializeObject(message));
        }

        /// <summary>
        /// Publishes the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<long> PublishAsync(T message)
        {
            return await Subscriber.PublishAsync(RedisChannel, JsonConvert.SerializeObject(message));
        }
    }
}
