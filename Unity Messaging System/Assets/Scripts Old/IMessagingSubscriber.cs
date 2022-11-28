namespace JGM.MessagingSystem
{
    public interface IMessagingSubscriber
    {
        void OnReceiveMessage(string message, object messageData);
    }
}