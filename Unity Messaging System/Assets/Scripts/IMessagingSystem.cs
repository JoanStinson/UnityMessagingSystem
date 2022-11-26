using System;

namespace JGM.MessagingSystem
{
    public interface IMessagingSystem
    {
        void Subscribe(string message, IMessagingSubscriber subscriber);
        void Unsubscribe(string message, IMessagingSubscriber subscriber);
        void Dispatch(string message, object messageData);
    }
}