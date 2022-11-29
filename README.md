# Unity Messaging System
A typesafe, lightweight Unity message bus system that respects the Open-Closed principle.

<p align="center">
  <a>
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <img alt="License" src="https://img.shields.io/github/license/JoanStinson/UnityMessagingSystem?logo=github">
  </a>
  <a>
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/JoanStinson/UnityMessagingSystem?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/JoanStinson/UnityMessagingSystem?logo=VirtualBox">
  </a>
  <a>
    <img alt="Downloads" src="https://img.shields.io/github/downloads/JoanStinson/UnityMessagingSystem/total?color=brightgreen">
  </a>
  <a>
    <img alt="Last Release" src="https://img.shields.io/github/v/release/JoanStinson/UnityMessagingSystem?include_prereleases&logo=Dropbox&color=yellow">
  </a>
</p>

<br>
<p align="center">
  <img src="https://github.com/JoanStinson/UnityMessagingSystem/blob/main/megaphone.PNG" width="30%" height="30%">
</p>

## 📣 How It Works
For starters, import the package located in the [Releases](https://github.com/JoanStinson/UnityMessagingSystem/releases) section into your project.

* ### Step 1 - Write a message. It can be a class or a struct.
```csharp
public readonly struct PlayerSpawnMessage
{
    public readonly int Health;
    public readonly float Speed;
}
```
> <b>Structs</b> are <b>preferred</b> to reduce GC work and because messages will 99% of the time only contain data.

If the message doesn't require data, you can have an empty class or struct too.
```csharp
public readonly struct PlayerDeathMessage { }
```

* ### Step 2 - Inherit from IMessagingSubscriber<<Type>> explicitly defining the message type and subscribe to it. 
```csharp
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
```
> You can make use of the <b>DefaultMessagingSystem singleton</b> via the <b>Instance</b> property. Nevertheless, I <b>prefer to inject the dependency</b>, so that later on I can mock it and do unit tests, apart from having a different messaging system implementation if it where required.

* ### Step 3 - Create a message instance and dispatch it.
```csharp
public class ExampleDispatcherClass : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MessagingSystem.Instance.Dispatch(new PlayerSpawnMessage(3, 5));
            MessagingSystem.Instance.Dispatch(new PlayerDeathMessage());
        }
    }
}
```
> The same as I said before, be sure to have the <b>same instance referenced</b>. Either by <b>injecting</b> it via a constructor/initialize method <b>or</b> making use of the <b>singleton Instance</b> (although less recommended for obvious reasons, unless you are a beginner).

## 🔍 Unit Tests
Unit tested with 100% code coverage to be certain the messaging system implementation works properly.
<p align="center">
  <img src="https://github.com/JoanStinson/UnityMessagingSystem/blob/main/tests.PNG">
</p>
