using IoCWebApp.Classes;
using IoCWebApp.DIContainer;
using Xunit;

namespace IoC_Test_Cases
{
    public class ContainerTest
    {
        private interface IMessage
        {
            string Test { get; }
        }

        private class Message : IMessage
        {
            public string Test => "foo";
        }

        [Fact]
        public void TestResolveTransient()
        {
            // Arrange
            var container = new Container();

            // Act
            container.Register<IMessage, Message>(Lifecycle.Transient);
            var instance1 = container.Resolve<Message>();
            var instance2 = container.Resolve<Message>();

            // Assert
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void TestResolveSingleton()
        {
            // Arrange
            var container = new Container();

            // Act
            container.Register<IMessage, Message>(Lifecycle.Singleton);
            var instance1 = container.Resolve<Message>();
            var instance2 = container.Resolve<Message>();

            // Assert
            Assert.Same(instance1, instance2);
        }
    }
}