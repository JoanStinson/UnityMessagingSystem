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

        public void Subscribe<T>(IMessagingSubscriber<T> subscriber)
        {
            string messageName = typeof(T).Name;

            if (!m_messages.ContainsKey(messageName))
            {
                m_messages[messageName] = new HashSet<IMessagingSubscriber>();
            }

            m_messages[messageName].Add(subscriber);
        }

        public void Unsubscribe<T>(IMessagingSubscriber<T> subscriber)
        {
            string messageName = typeof(T).Name;

            if (m_messages.ContainsKey(messageName))
            {
                m_messages[messageName].Remove(subscriber);
            }
        }

        public void Dispatch<T>(T message)
        {
            string messageName = typeof(T).Name;

            if (m_messages.ContainsKey(messageName))
            {
                foreach (var subscriber in m_messages[messageName])
                {
                    (subscriber as IMessagingSubscriber<T>).OnReceiveMessage(message);
                }
            }
        }
    }
}