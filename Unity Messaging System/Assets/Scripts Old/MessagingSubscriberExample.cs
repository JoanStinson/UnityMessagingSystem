using UnityEngine;

namespace JGM.MessagingSystem
{
    public class MessageSubscriberExample : MonoBehaviour, IMessagingSubscriber
    {
        private readonly IMessagingSystem messagingSystem = new DefaultMessagingSystem();

        private void OnEnable()
        {
            messagingSystem.Subscribe("PlayerDeath", this);
            messagingSystem.Subscribe("PlayerRespawn", this);
        }

        private void OnDisable()
        {
            messagingSystem.Unsubscribe("PlayerDeath", this);
            messagingSystem.Unsubscribe("PlayerRespawn", this);
        }

        public void OnReceiveMessage(string message, object messageData)
        {
            // solution: dictionary, doble despacho, interfaces
            if (message == "PlayerDeath")
            {
                Debug.Log("Player Death message received");
            }
            else if (message == "PlayerRespawn")
            {
                Debug.Log("Player Respawn message received");
            }
        }
    }
}