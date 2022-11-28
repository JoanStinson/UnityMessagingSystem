using UnityEngine;

namespace JGM.MessagingSystem
{
    public class MessagingSubscriberExample : MonoBehaviour,
        IMessagingSubscriber<PlayerSpawnMessage>,
        IMessagingSubscriber<PlayerDeathMessage>
    {
        private readonly IMessagingSystem messagingSystem = new DefaultMessagingSystem();

        private void OnEnable()
        {
            messagingSystem.Subscribe<PlayerSpawnMessage>(this);
            messagingSystem.Subscribe<PlayerDeathMessage>(this);
        }

        private void OnDisable()
        {
            messagingSystem.Unsubscribe<PlayerSpawnMessage>(this);
            messagingSystem.Unsubscribe<PlayerDeathMessage>(this);
        }

        public void OnReceiveMessage(PlayerSpawnMessage message)
        {
            Debug.Log(message.ToString());
        }

        public void OnReceiveMessage(PlayerDeathMessage message)
        {
            Debug.Log("Received Player Death message");
        }
    }
}