using System;
using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface ISubscribeToMessages
    {
        string Channel { get; }

        void Subscribe(Action<Message> callback);
        Task SubscribeAsync(Action<Message> callback);
    }
}