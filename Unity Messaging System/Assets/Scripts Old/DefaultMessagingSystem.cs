using System.Collections.Generic;

namespace JGM.MessagingSystem
{
    public class DefaultMessagingSystem : IMessagingSystem
    {
        private readonly Dictionary<string, HashSet<IMessagingSubscriber>> m_messages;

        public DefaultMessagingSystem()
        {
            m_messages = new Dictionary<string, HashSet<IMessagingSubscriber>>();
        }

        public void Subscribe(string message, IMessagingSubscriber subscriber)
        {
            if (!m_messages.ContainsKey(message))
            {
                m_messages[message] = new HashSet<IMessagingSubscriber>();
            }

            m_messages[message].Add(subscriber);
        }

        public void Unsubscribe(string message, IMessagingSubscriber subscriber)
        {
            if (m_messages.ContainsKey(message))
            {
                m_messages[message].Remove(subscriber);
            }
        }

        public void Dispatch(string message, object messageData)
        {
            if (m_messages.ContainsKey(message))
            {
                foreach (var subscriber in m_messages[message])
                {
                    subscriber.OnReceiveMessage(message, messageData);
                }
            }
        }
    }
}