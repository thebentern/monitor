using Microsoft.AspNet.SignalR;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System;
using System.Configuration;

namespace Monitor.Dashboard.Nancy.Hubs
{
    public class MessageHub : Hub
    {
        public ISubscribeToMessages<DefaultMessage> Messages =
            new RedisMessageSubscriber<DefaultMessage>(
                ConfigurationManager.AppSettings["RedisHost"] ?? "localhost", "Default");

        public MessageHub()
        {
              Messages.Subscribe(Broadcast);
        }
        private void Broadcast(IMessage message)
        {
            Clients.All.publishMesage(message.Channel, message.Content);
        }
        //Console.WriteLine($"Broadcasting on channel '{m.Channel}': {m.Content}");
    }
}
