using System.Collections.Generic;

namespace JGM.MessagingSystem
{
    public class DefaultMessagingSystem : IMessagingSystem
    {
        private readonly Dictionary<string, HashSet<IMessagingSubscriber>> m_subscribers;

        public DefaultMessagingSystem()
        {
            m_subscribers = new Dictionary<string, HashSet<IMessagingSubscriber>>();
        }

        public void Subscribe(string message, IMessagingSubscriber subscriber)
        {
            if (!m_subscribers.ContainsKey(message))
            {
                m_subscribers[message] = new HashSet<IMessagingSubscriber>();
            }

            m_subscribers[message].Add(subscriber);
        }

        public void Unsubscribe(string message, IMessagingSubscriber subscriber)
        {
            if (m_subscribers.ContainsKey(message))
            {
                m_subscribers[message].Remove(subscriber);
            }
        }

        public void Dispatch(string message, object messageData)
        {
            if (m_subscribers.ContainsKey(message))
            {
                foreach (var subscriber in m_subscribers[message])
                {
                    subscriber.OnReceiveMessage(message, messageData);
                }
            }
        }
    }
}