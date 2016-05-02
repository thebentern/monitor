using System;
using System.Threading.Tasks;

namespace Monitor.Core
{
    /// <summary>
    /// Interface for subscribing to messages
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubscribeToMessages<T> where T : IMessage
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        string Channel { get; }

        /// <summary>
        /// Subscribes to Messages
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        void Subscribe(Action<T> callback);
        /// <summary>
        /// Subscribes to Messages Asynchronously
        /// </summary>
        /// <param name="callback">The message received callback action.</param>
        /// <returns></returns>
        Task SubscribeAsync(Action<T> callback);
    }
}