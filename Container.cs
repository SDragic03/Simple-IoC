using System;
using System.Collections.Generic;
using System.Linq;
using IoCWebApp.Classes;

namespace IoCWebApp.DIContainer
{
    public class Container
    {
        public Container()
        {
            _registrations = new List<ContainerItem>();
        }

        #region Methods

        public void Register<TAbstract, TConcrete>(Lifecycle lifecycle) where TConcrete : TAbstract, new()
        {
            var abstractionType = typeof(TAbstract);
            var concreteType = typeof(TConcrete);

            Register(abstractionType, concreteType, lifecycle);
        }

        public object Resolve(Type objType)
        {
            return GetConcreteType(objType);
        }

        public T Resolve<T>() where T : class
        {
            var type = typeof(T);

            return (T)GetConcreteType(type);
        }

        private void Register(Type abstractionType, Type concreteType, Lifecycle lifecycleType)
        {
            if (!abstractionType.IsInterface)
                throw new ApplicationException("First generic argument must be an inteface type.");

            _registrations.Add(new ContainerItem { AbstractionType = abstractionType, ConcreteType = concreteType, Lifecycle = lifecycleType });
        }

        private object GetConcreteType(Type typeToResolve)
        {
            var containerItem = _registrations.FirstOrDefault(item => item.ConcreteType == typeToResolve);

            if (containerItem == null)
                throw new Exception($"The type {typeToResolve.Name} has not been registered");

            if (containerItem.Lifecycle == Lifecycle.Singleton)
                return GetSingleton(containerItem.ConcreteType);

            return GetTypeInstance(containerItem.ConcreteType);
        }

        public object GetSingleton(Type type)
        {
            lock (SyncSingleton)
            {
                var singleton = SingletonStorage.Get(type);

                if (singleton == null)
                {
                    singleton = GetTypeInstance(type);
                    SingletonStorage.Add(singleton);
                }
                return singleton;
            }
        }

        private object GetTypeInstance(Type type)
        {
            object instance = null;

            var constructors = type.GetConstructors();
            if (constructors.Length > 0)
            {
                var constructor = constructors[0];

                var constructorArguments = new List<object>();
                var parameters = constructor.GetParameters();

                foreach (var parameter in parameters)
                {
                    object parameterInstance = null;

                    if (parameter.ParameterType.IsInterface)
                        parameterInstance = GetConcreteType(parameter.ParameterType);

                    constructorArguments.Add(parameterInstance);
                }

                instance = Activator.CreateInstance(type, constructorArguments.ToArray());
            }

            return instance;
        }

        #endregion

        #region Fields

        private static readonly object SyncSingleton = new object();
        private readonly List<ContainerItem> _registrations;

        #endregion

    }
}
