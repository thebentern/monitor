using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface IPublishMessages<T> where T : IMessage
    {
        string Channel { get; }
        string Origin { get; }

        long Publish( T message );
        Task<long> PublishAsync( T message );
    }
}