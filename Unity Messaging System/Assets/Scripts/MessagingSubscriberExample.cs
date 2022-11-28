using UnityEngine;

namespace JGM.MessagingSystem
{
    public class MessagingSubscriberExample : MonoBehaviour,
        IMessagingSubscriber<PlayerSpawnMessage>,
        IMessagingSubscriber<PlayerDeathMessage>
    {
        private void OnEnable()
        {
            MessagingSystem.Instance.Subscribe<PlayerSpawnMessage>(this);
            MessagingSystem.Instance.Subscribe<PlayerDeathMessage>(this);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.Unsubscribe<PlayerSpawnMessage>(this);
            MessagingSystem.Instance.Unsubscribe<PlayerDeathMessage>(this);
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