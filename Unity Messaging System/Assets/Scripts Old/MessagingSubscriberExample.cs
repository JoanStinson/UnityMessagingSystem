using UnityEngine;

namespace JGM.MessagingSystem
{
    public class MessageSubscriberExample : MonoBehaviour, IMessagingSubscriber
    {
        private void OnEnable()
        {
            MessagingSystem.Instance.Subscribe("PlayerDeath", this);
            MessagingSystem.Instance.Subscribe("PlayerRespawn", this);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.Unsubscribe("PlayerDeath", this);
            MessagingSystem.Instance.Unsubscribe("PlayerRespawn", this);
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