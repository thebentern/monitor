namespace Monitor.Core
{
    public interface IPublishMessages
    {
        void Publish(Message message);
    }
}