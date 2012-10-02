// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoFactoryRegistration.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the contract for the fluent interface for registering auto factories.
    /// </summary>
    /// <typeparam name="T">
    /// The interface of the type to be created by the factory.
    /// </typeparam>
    public interface IAutoFactoryRegistration<T>
    {
        #region Public Properties

        /// <summary>
        /// Gets the target Unity container on which to perform the registrations.
        /// </summary>
        IUnityContainer Container { get; }

        /// <summary>
        /// Gets the lifetime to be assigned to the <see cref="IUnityFactory{TParam,T}"/>.
        /// </summary>
        LifetimeManager FactoryLifetimeManager { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Registers an <see cref="IUnityFactory{TParam,T}">auto factory</see> which takes one typed parameter.
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter passed to the constructed type.</typeparam>
        void WithParam<TParam>();

        /// <summary>
        /// Registers an <see cref="IUnityFactory{TParam1,TParam2,T}">auto factory</see> which takes two typed parameters.
        /// </summary>
        /// <typeparam name="TParam1">The type of the first parameter passed to the constructed type.</typeparam>
        /// <typeparam name="TParam2">The type of the second parameter passed to the constructed type.</typeparam>
        void WithParams<TParam1, TParam2>();

        /// <summary>
        /// Registers an <see cref="IUnityFactory{T}">auto factory</see>.
        /// </summary>
        void WithoutParameters();

        #endregion
    }
}