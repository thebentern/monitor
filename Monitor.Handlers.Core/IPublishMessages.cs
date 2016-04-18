using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface IPublishMessages
    {
        string Channel { get; }
        string Origin { get; }

        long Publish( IMessage message );
        Task<long> PublishAsync( IMessage message );
    }
}