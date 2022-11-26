using System.Collections;
using UnityEngine;

namespace JGM.MessagingSystem
{
    public interface IMessagingSubscriber 
    {
        void OnReceiveMessage(object message);
    }
}