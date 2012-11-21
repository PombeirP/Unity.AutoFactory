// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypedFactoryRegistration.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Unity.AutoFactory
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the contract for the fluent interface for registering auto factories.
    /// </summary>
    public interface ITypedFactoryRegistration
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
        /// Defines the concrete type which the factory will create.
        /// </summary>
        void UsingConcreteType<TTo>();

        #endregion
    }
}