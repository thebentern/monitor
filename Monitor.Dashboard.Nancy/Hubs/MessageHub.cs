using Microsoft.AspNet.SignalR;
using Monitor.Core;
using Monitor.Handlers.Redis;
using System;

namespace Monitor.Dashboard.Nancy.Hubs
{
    public class MessageHub : Hub
    {
        public ISubscribeToMessages Messages = new RedisMessageSubscriber("localhost", "Default");
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
