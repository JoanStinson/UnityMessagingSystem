using JGM.MessagingSystem;
using Moq;
using NUnit.Framework;

namespace JGM.MessagingSystemTests
{
    public class MessagingSystemTests
    {
        [Test]
        public void When_SubscribedAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            IMessagingSystem messagingSystem = new DefaultMessagingSystem();
            Mock<IMessagingSubscriber> messagingSubscriber = new Mock<IMessagingSubscriber>();
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            messagingSubscriber.Verify(mock => mock.OnReceiveMessage(It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            IMessagingSystem messagingSystem = new DefaultMessagingSystem();
            Mock<IMessagingSubscriber> messagingSubscriber = new Mock<IMessagingSubscriber>();
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Unsubscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            messagingSubscriber.Verify(mock => mock.OnReceiveMessage(It.IsAny<object>()), Times.Never());
        }

        [Test]
        public void When_SubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            IMessagingSystem messagingSystem = new DefaultMessagingSystem();
            Mock<IMessagingSubscriber> messagingSubscriber = new Mock<IMessagingSubscriber>();
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            messagingSubscriber.Verify(mock => mock.OnReceiveMessage(It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            IMessagingSystem messagingSystem = new DefaultMessagingSystem();
            Mock<IMessagingSubscriber> messagingSubscriber = new Mock<IMessagingSubscriber>();
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Unsubscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Unsubscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            messagingSubscriber.Verify(mock => mock.OnReceiveMessage(It.IsAny<object>()), Times.Never());
        }

        [Test]
        public void When_SubscribedToTwoMessagesAndMessagesAreDispatched_SubscriberMethodsGetCalledOnce()
        {
            IMessagingSystem messagingSystem = new DefaultMessagingSystem();
            Mock<IMessagingSubscriber> messagingSubscriber = new Mock<IMessagingSubscriber>();
            messagingSystem.Subscribe("PlayerDeath", messagingSubscriber.Object);
            messagingSystem.Subscribe("PlayerRespawn", messagingSubscriber.Object);
            messagingSystem.Dispatch("PlayerDeath", 6);
            messagingSystem.Dispatch("PlayerRespawn", 7);
            messagingSubscriber.Verify(mock => mock.OnReceiveMessage(It.IsAny<object>()), Times.Exactly(2));
        }
    }
}