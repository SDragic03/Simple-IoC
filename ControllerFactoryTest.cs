using System.Web.Mvc;
using IoCWebApp.Classes;
using IoCWebApp.Controllers;
using IoCWebApp.Factories;
using Xunit;

namespace IoC_Test_Cases
{
    public class ControllerFactoryTest
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
            //Arrange
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));

            // Act
            ControllerFactory.Container.Register<IMessage, Message>(Lifecycle.Transient);
            var instance1 = ControllerFactory.Container.Resolve(typeof(Message));
            var instance2 = ControllerFactory.Container.Resolve(typeof(Message));

            // Assert
            Assert.NotSame(instance1, instance2);
        }

        [Fact]
        public void TestResolveSingleton()
        {
            //Arrange
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));

            // Act
            ControllerFactory.Container.Register<IMessage, Message>(Lifecycle.Singleton);
            var instance1 = ControllerFactory.Container.Resolve(typeof(Message));
            var instance2 = ControllerFactory.Container.Resolve(typeof(Message));

            // Assert
            Assert.Same(instance1, instance2);
        }
    }
}
