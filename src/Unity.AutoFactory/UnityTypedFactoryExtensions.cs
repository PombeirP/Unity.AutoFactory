// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityTypedFactoryExtensions.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Unity.AutoFactory
{
    using System;

    using Microsoft.Practices.Unity;

    using Unity.AutoFactory.Implementation;

    /// <summary>
    /// Defines extension methods for providing custom typed factories based on a factory interface.
    /// </summary>
    public static class UnityTypedFactoryExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Registers a typed factory.
        /// </summary>
        /// <typeparam name="TFactory">
        /// The <see cref="Type"/> of the factory interface.
        /// </typeparam>
        /// <typeparam name="TFactoryImpl">
        /// The concrete <see cref="Type"/> of the factory (TODO: This type should be automatically generated for the user).
        /// </typeparam>
        /// <param name="container">
        /// The Unity container.
        /// </param>
        /// <returns>
        /// The holder object which facilitates the fluent interface.
        /// </returns>
        public static ITypedFactoryRegistration RegisterAutoFactory<TFactory, TFactoryImpl>(this IUnityContainer container)
            where TFactoryImpl : class, TFactory
        {
            return container.RegisterAutoFactory<TFactory, TFactoryImpl>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers a typed factory.
        /// </summary>
        /// <typeparam name="TFactory">
        /// The <see cref="Type"/> of the factory interface.
        /// </typeparam>
        /// <typeparam name="TFactoryImpl">
        /// The concrete <see cref="Type"/> of the factory (TODO: This type should be automatically generated for the user).
        /// </typeparam>
        /// <param name="container">
        /// The Unity container.
        /// </param>
        /// <param name="factoryLifetimeManager">
        /// The <see cref="LifetimeManager"/> that controls the lifetime of the <see cref="TFactory"/> factory.
        /// </param>
        /// <returns>
        /// The holder object which facilitates the fluent interface.
        /// </returns>
        public static ITypedFactoryRegistration RegisterAutoFactory<TFactory, TFactoryImpl>(this IUnityContainer container, LifetimeManager factoryLifetimeManager)
            where TFactoryImpl : class, TFactory
        {
            var typedFactoryRegistration = new TypedFactoryRegistration<TFactory, TFactoryImpl>(container, factoryLifetimeManager);
            return typedFactoryRegistration;
        }

        #endregion
    }
}