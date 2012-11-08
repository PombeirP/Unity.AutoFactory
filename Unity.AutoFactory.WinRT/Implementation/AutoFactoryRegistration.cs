// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFactoryRegistration.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory.Implementation
{
    using System;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Implements the fluent interface for registering auto factories.
    /// </summary>
    /// <typeparam name="T">
    /// The interface of the type to be created by the factory.
    /// </typeparam>
    internal class AutoFactoryRegistration<T> : IAutoFactoryRegistration<T>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFactoryRegistration{T}"/> class.
        /// </summary>
        /// <param name="container">
        /// The target Unity container on which to perform the registrations.
        /// </param>
        /// <param name="lifetimeManager">
        /// The lifetime manager for the resulting auto factory.
        /// </param>
        public AutoFactoryRegistration(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            this.Container = container;
            this.FactoryLifetimeManager = lifetimeManager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the target Unity container on which to perform the registrations.
        /// </summary>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// Gets the lifetime to be assigned to the <see cref="IUnityFactory{TParam,T}"/>.
        /// </summary>
        public LifetimeManager FactoryLifetimeManager { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Registers an <see cref="IUnityFactory{TParam,T}">auto factory</see> which takes one typed parameter.
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter passed to the constructed type.</typeparam>
        public void WithParam<TParam>()
        {
            this.Container.RegisterType<IUnityFactory<TParam, T>, UnityFactory1<TParam, T>>(this.FactoryLifetimeManager);

            this.Container.RegisterInstance<Func<TParam, T>>(
                param =>
                this.Container.Resolve<T>(
                    new DependencyOverride(
                    typeof(TParam), new InjectionParameter(param))));
        }

        /// <summary>
        /// Registers an <see cref="IUnityFactory{TParam1,TParam2,T}">auto factory</see> which takes two typed parameters.
        /// </summary>
        /// <typeparam name="TParam1">The type of the first parameter passed to the constructed type.</typeparam>
        /// <typeparam name="TParam2">The type of the second parameter passed to the constructed type.</typeparam>
        public void WithParams<TParam1, TParam2>()
        {
            this.Container.RegisterType<IUnityFactory<TParam1, TParam2, T>, UnityFactory2<TParam1, TParam2, T>>(this.FactoryLifetimeManager);

            this.Container.RegisterInstance<Func<TParam1, TParam2, T>>(
                (param1, param2) =>
                this.Container.Resolve<T>(
                    new DependencyOverride(typeof(TParam1), new InjectionParameter(param1)), 
                    new DependencyOverride(typeof(TParam2), new InjectionParameter(param2))));
        }

        /// <summary>
        /// Registers an <see cref="IUnityFactory{T}">auto factory</see>.
        /// </summary>
        public void WithoutParameters()
        {
            this.Container.RegisterType<IUnityFactory<T>, UnityFactory0<T>>(this.FactoryLifetimeManager);
        }

        #endregion
    }
}