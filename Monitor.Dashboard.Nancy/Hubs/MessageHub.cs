using Microsoft.AspNet.SignalR;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System.Configuration;

namespace Monitor.Dashboard.Nancy.Hubs
{
    /// <summary>
    /// SignalR hub for Message subscription
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// The Message subscription
        /// </summary>
        public ISubscribeToMessages<DefaultMessage> Messages =
            new RedisMessageSubscriber<DefaultMessage>(
                ConfigurationManager.AppSettings["RedisHost"] ?? "localhost", "Default");

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHub"/> class.
        /// </summary>
        public MessageHub()
        {
            Messages.Subscribe(Broadcast);
        }
        /// <summary>
        /// Broadcasts the specified message to all subscribed clients.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Broadcast(IMessage message)
        {
            Clients.All.publishMesage(message.Channel, message.Content);
        }
        //Console.WriteLine($"Broadcasting on channel '{m.Channel}': {m.Content}");
    }
}
