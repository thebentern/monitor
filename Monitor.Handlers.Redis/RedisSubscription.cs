using StackExchange.Redis;
using System;

namespace Monitor.Handlers.Redis
{
    /// <summary>
    /// Redis subscription base functionality
    /// </summary>
    public class RedisSubscription : IDisposable
    {
        private readonly ConnectionMultiplexer _redis;

        protected ISubscriber Subscriber;
        protected RedisChannel RedisChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisSubscription"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="channel">The channel.</param>
        public RedisSubscription(string host, string channel)
        {
            Channel = channel;
            _redis = ConnectionMultiplexer.Connect(host);
            RedisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Literal);
            Subscriber = _redis.GetSubscriber();
        }

        /// <summary>
        /// Gets the message subscription channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public string Channel { get; }

        /// <summary>
        /// Releases resources.
        /// </summary>
        public void Dispose() => _redis?.Close();
    }
}
