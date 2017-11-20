using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace MintFinancialExport.Core
{
    /// <summary>
    /// Service locator wrapper for the StructureMap IoC container.
    /// </summary>
    public static class ServiceLocator
    {
        private readonly static Container IocContainer = new Container();

        /// <summary>
        /// Gets a reference to the underlying IoC container.
        /// </summary>
        /// <returns>A reference to the container via its interface.</returns>
        public static IContainer GetContainer()
        {
            return IocContainer;
        }

        /// <summary>
        /// Adds a registry to the StructureMap IoC container
        /// </summary>
        /// <param name="registry">The registry to be added.</param>
        public static void Register(Registry registry)
        {
            IocContainer.Configure(c => c.AddRegistry(registry));
        }

        /// <summary>
        /// Adds a single registration to the IoC container.
        /// </summary>
        /// <typeparam name="TPlugin"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        public static void Register<TPlugin, TConcrete>() where TConcrete : TPlugin
        {
            IocContainer.Configure(c => c.For<TPlugin>().Use<TConcrete>());
        }

        /// <summary>
        /// Adds a single registration to the IoC container.
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="concreteType"></param>
        public static void Register(Type pluginType, Type concreteType)
        {
            IocContainer.Configure(c => c.For(pluginType).Use(concreteType));
        }

        /// <summary>
        /// Returns a boolean value indicating if the IoC container has a registration for the specified type.
        /// </summary>
        /// <param name="serviceType">The type being queried.</param>
        /// <returns>A flag indicating if the type is registered.</returns>
        public static bool HasRegistrationFor(Type serviceType)
        {
            return IocContainer.TryGetInstance(serviceType) != null;
        }

        /// <summary>
        /// Returns a boolean value indicating if the IoC container has a registration for the specified type.
        /// </summary>
        /// <typeparam name="TService">The type being queried.</typeparam>
        /// <returns>A flag indicating if the type is registered.</returns>
        public static bool HasRegistrationFor<TService>() where TService : class
        {

            return IocContainer.TryGetInstance<TService>() != null;
        }

        /// <summary>
        /// Returns the type of the specified plugin type, based on its full name.
        /// </summary>
        /// <param name="fullPluginName">The full name of the type being queried.</param>
        /// <returns>
        /// If the container has a registration for the type in question, then it will return the type of the 
        /// specified plugin type; else it will return null.
        /// </returns>
        public static Type GetPluginType(string fullPluginName)
        {
            var pluginType =
                IocContainer.Model.PluginTypes.FirstOrDefault(x => x.PluginType.FullName == fullPluginName);

            if (pluginType != null)
            {
                return pluginType.PluginType;
            }

            return null;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>An instance of an object of type <paramref name="serviceType"/>.</returns>
        public static object GetInstance(Type serviceType)
        {
            return IocContainer.GetInstance(serviceType);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>An instance of an object of type <paramref name="serviceType"/>.</returns>
        public static object GetInstance(Type serviceType, string key)
        {
            return IocContainer.TryGetInstance(serviceType, key);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>An instance of an object of type <typeparamref name="TService"/> .</returns>
        public static TService GetInstance<TService>()
        {
            return IocContainer.GetInstance<TService>();
        }

        /// <summary>
        /// Gets the specified service from the container, using the specified key.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>An instance of an object of type <typeparamref name="TService"/> .</returns>
        public static TService GetInstance<TService>(string key)
        {
            return IocContainer.TryGetInstance<TService>(key);
        }

        /// <summary>
        /// Gets all instances.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>All instances of type <paramref name="serviceType"/>.</returns>
        public static IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var instances = IocContainer.GetAllInstances(serviceType);

            return instances.Cast<object>().ToList();
        }

        /// <summary>
        /// Gets all instances.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>All instances of type <typeparamref name="TService"/> .</returns>
        public static IEnumerable<TService> GetAllInstances<TService>()
        {
            return IocContainer.GetAllInstances<TService>();
        }

        /// <summary>
        /// Adds an item to the IoC container with a specified name.
        /// </summary>
        /// <typeparam name="T">The type of the object being added.</typeparam>
        /// <param name="item">The item being added.</param>
        /// <param name="itemName">The name of the item.</param>
        public static void AddItem<T>(T item, string itemName) where T : class
        {
            IocContainer.Configure(x => x.For<T>().Use(item).Named(itemName));
        }

        /// <summary>
        /// Adds a single typed instance to the IoC container's registrations.
        /// </summary>
        /// <param name="serviceType">The type of the object being added.</param>
        /// <param name="service">The concrete object being added.</param>
        public static void AddItem(Type serviceType, object service)
        {
            IocContainer.Configure(c => c.For(serviceType).Use(service));
        }

        /// <summary>
        /// Adds a single typed instance to the IoC container's registrations.
        /// </summary>
        /// <typeparam name="TService">The type of the object being added.</typeparam>
        /// <param name="service">The concrete object being added.</param>
        public static void AddItem<TService>(TService service) where TService : class
        {
            IocContainer.Configure(c => c.For<TService>().Use(service));
        }

        /// <summary>
        /// Removes all instances of the specified type from the IoC container.
        /// </summary>
        /// <typeparam name="TService">The type to be ejected from the container.</typeparam>
        public static void EjectAllInstancesOf<TService>()
        {
            IocContainer.EjectAllInstancesOf<TService>();
        }
    }
}
