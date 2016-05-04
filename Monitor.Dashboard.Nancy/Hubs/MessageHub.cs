using System.Configuration;

using Microsoft.AspNet.SignalR;

using Monitor.Core;
using Monitor.Handlers.Redis;

namespace Monitor.Dashboard.Nancy.Hubs
{
    /// <summary>
    /// SignalR hub for Message subscription
    /// </summary>
    public class MessageHub : Hub
    {
        private readonly ISubscribeToMessages<DefaultMessage> messages =
            new RedisMessageSubscriber<DefaultMessage>(
                ConfigurationManager.AppSettings["RedisHost"] ?? "localhost", "Default");

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHub"/> class.
        /// </summary>
        public MessageHub()
        {
            this.messages.Subscribe(this.Broadcast);
        }

        /// <summary>
        /// Broadcasts the specified message to all subscribed clients.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Broadcast(IMessage message)
        {
            Clients.All.publishMesage(message.Channel, message.Content);

            // Console.WriteLine($"Broadcasting on channel '{m.Channel}': {m.Content}"); //For debugging
        }
    }
}
