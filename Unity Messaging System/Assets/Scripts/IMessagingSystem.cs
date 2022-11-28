namespace JGM.MessagingSystem
{
    public interface IMessagingSystem
    {
        void Subscribe<T>(IMessagingSubscriber<T> subscriber);
        void Unsubscribe<T>(IMessagingSubscriber<T> subscriber);
        void Dispatch<T>(T message);
    }
}