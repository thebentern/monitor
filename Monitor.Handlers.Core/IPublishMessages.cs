using System.Threading.Tasks;

namespace Monitor.Core
{
    public interface IPublishMessages
    {
        string Channel { get; }
        string Origin { get; }

        long Publish(Message message);
        Task<long> PublishAsync(Message message);
    }
}