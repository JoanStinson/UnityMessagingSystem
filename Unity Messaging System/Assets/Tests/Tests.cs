using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests
{
    // can subscribe and receive event(s) when triggered
    // can unscubscribe from events so that are not called when triggered

    [Test]
    public void When_SubscribedToEventAndEventIsTriggered_SubscriberMethodGetsCalledOnce()
    {
        IMessagingSystem messagingSystem = new DefaultMessagingSystem();
        ExampleClass exampleClass.Subscribe("PlayerDeath");
        messagingSystem.SendEvent("PlayerDeath");

    }
}