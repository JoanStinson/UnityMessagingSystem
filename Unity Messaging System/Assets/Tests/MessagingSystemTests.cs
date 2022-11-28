using JGM.MessagingSystem;
using Moq;
using NUnit.Framework;

namespace JGM.MessagingSystemTests
{
    public class MessagingSystemTests
    {
        private IMessagingSystem messagingSystem;
        private Mock<IMessagingSubscriber<PlayerDeathMessage>> playerDeathSubscriberMock;

        [SetUp]
        public void SetUp()
        {
            messagingSystem = new DefaultMessagingSystem();
            playerDeathSubscriberMock = new Mock<IMessagingSubscriber<PlayerDeathMessage>>();
        }

        [Test]
        public void When_SubscribedAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Unsubscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Never());
        }

        [Test]
        public void When_SubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledOnce()
        {
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Once());
        }

        [Test]
        public void When_SubscribedThenUnsubscribedTwiceAndMessageDispatched_SubscriberMethodGetsCalledNever()
        {
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Unsubscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Unsubscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Never());
        }

        [Test]
        public void When_TwoSubscribersSubscribedToTwoMessagesAndMessagesAreDispatched_SubscribersMethodsGetCalledOnce()
        {
            var playerSpawnSubscriberMock = new Mock<IMessagingSubscriber<PlayerSpawnMessage>>();
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Subscribe(playerSpawnSubscriberMock.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            messagingSystem.Dispatch(new PlayerSpawnMessage(3, 5));
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Once());
            playerSpawnSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerSpawnMessage>()), Times.Once());
        }

        [Test]
        public void When_TwoSubscribersAreSubscribedToSameMessageAndMessageIsDispatched_SubscribersMethodsGetCalledOnce()
        {
            var playerDeathSubscriberMock2 = new Mock<IMessagingSubscriber<PlayerDeathMessage>>();
            messagingSystem.Subscribe(playerDeathSubscriberMock.Object);
            messagingSystem.Subscribe(playerDeathSubscriberMock2.Object);
            messagingSystem.Dispatch(new PlayerDeathMessage());
            playerDeathSubscriberMock.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Once());
            playerDeathSubscriberMock2.Verify(mock => mock.OnReceiveMessage(It.IsAny<PlayerDeathMessage>()), Times.Once());
        }
    }
}