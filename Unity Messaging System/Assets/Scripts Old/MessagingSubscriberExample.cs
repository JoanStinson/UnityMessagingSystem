namespace JGM.MessagingSystem
{
    public class MessageSubscriberExample : IMessagingSubscriber
    {
        public void OnReceiveMessage(string message, object messageData)
        {
            // solution: dictionary, doble despacho, interfaces
            if (message == "3")
            {

            }
            else if (message == "5")
            {

            }
        }
    }
}