namespace JGM.MessagingSystem
{
    public interface IMessagingSubscriber { }

    public interface IMessagingSubscriber<in T> : IMessagingSubscriber
    {
        void OnReceiveMessage(T message);
    }
}