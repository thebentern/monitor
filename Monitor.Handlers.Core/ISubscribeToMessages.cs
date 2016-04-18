using System;
using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface ISubscribeToMessages
    {
        string Channel { get; }

        void Subscribe( Action<IMessage> callback );
        Task SubscribeAsync( Action<IMessage> callback );
    }
}