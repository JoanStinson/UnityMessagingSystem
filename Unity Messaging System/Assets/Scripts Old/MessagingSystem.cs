using System;
using System.Collections.Generic;

namespace JGM.MessagingSystem
{
    public class MessagingSystem : IMessagingSystem
    {
        public static MessagingSystem Instance => instance.Value;
        private static readonly Lazy<MessagingSystem> instance = new Lazy<MessagingSystem>(() => new MessagingSystem());

        private readonly Dictionary<string, HashSet<IMessagingSubscriber>> m_messages;

        public MessagingSystem()
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