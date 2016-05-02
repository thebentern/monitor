using System.Threading.Tasks;

namespace Monitor.Core
{
    /// <summary>
    /// Interface for publishing messages
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPublishMessages<T> where T : IMessage
    {
        /// <summary>
        /// Gets the message subscription Channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        string Channel { get; }
        /// <summary>
        /// Gets the message subscription Origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        string Origin { get; }

        /// <summary>
        /// Publishes the specified Message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        long Publish(T message);

        /// <summary>
        /// Publishes the specified Message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<long> PublishAsync(T message);
    }
}