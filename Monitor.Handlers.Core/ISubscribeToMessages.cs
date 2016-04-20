using System;
using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface ISubscribeToMessages<T> where T : IMessage
    {
        string Channel { get; }

        void Subscribe( Action<T> callback );
        Task SubscribeAsync( Action<T> callback );
    }
}