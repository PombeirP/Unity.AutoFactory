// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityExtensions.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory
{
    using System;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines extension methods for providing <see cref="IUnityContainer"/> with auto-factory registration support.
    /// </summary>
    public static class UnityExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Registers an auto factory which takes one typed parameter.
        /// </summary>
        /// <typeparam name="TFrom">
        /// <see cref="Type"/> that will be requested.
        /// </typeparam>
        /// <typeparam name="TTo">
        /// <see cref="Type"/> that will actually be returned.
        /// </typeparam>
        /// <param name="container">
        /// The Unity container.
        /// </param>
        /// <returns>
        /// The holder object which facilitates the fluent interface.
        /// </returns>
        public static IAutoFactoryRegistration<TFrom> RegisterAutoFactoryFor<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
        {
            return container.RegisterAutoFactoryFor<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers an auto factory which takes one typed parameter.
        /// </summary>
        /// <typeparam name="TFrom">
        /// <see cref="Type"/> that will be requested.
        /// </typeparam>
        /// <typeparam name="TTo">
        /// <see cref="Type"/> that will actually be returned.
        /// </typeparam>
        /// <param name="container">
        /// The Unity container.
        /// </param>
        /// <param name="factoryLifetimeManager">
        /// The <see cref="LifetimeManager"/> that controls the lifetime of the factory.
        /// </param>
        /// <returns>
        /// The holder object which facilitates the fluent interface.
        /// </returns>
        public static IAutoFactoryRegistration<TFrom> RegisterAutoFactoryFor<TFrom, TTo>(this IUnityContainer container, LifetimeManager factoryLifetimeManager) where TTo : TFrom
        {
            var autoFactoryRegistration = new AutoFactoryRegistration<TFrom>(container, factoryLifetimeManager);

            container.RegisterType<TFrom, TTo>();

            return autoFactoryRegistration;
        }

        #endregion
    }
}