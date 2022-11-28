using JGM.MessagingSystem;
using Moq;
using NUnit.Framework;

namespace JGM.MessagingSystemTests
{
    public class MessagingSystemTests
    {
        private IMessagingSystem messagingSystem;
        private Mock<IMessagingSubscriber> subscriberMock;

        [SetUp]
        public void SetUp()
        {
            messagingSystem = new MessagingSystem.MessagingSystem();
            subscriberMock = new Mock<IMessagingSubscriber>();
        }

        [Test]
        public void When_SubscribedAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            subscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<string>(), It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Unsubscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            subscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
        }

        [Test]
        public void When_SubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            subscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<string>(), It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Unsubscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Unsubscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Dispatch("PlayerDeath", 5);
            subscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
        }

        [Test]
        public void When_SubscribedToTwoMessagesAndMessagesAreDispatched_SubscriberMethodsGetCalledOnce()
        {
            messagingSystem.Subscribe("PlayerDeath", subscriberMock.Object);
            messagingSystem.Subscribe("PlayerRespawn", subscriberMock.Object);
            messagingSystem.Dispatch("PlayerDeath", 6);
            messagingSystem.Dispatch("PlayerRespawn", 7);
            subscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
        }
    }
}