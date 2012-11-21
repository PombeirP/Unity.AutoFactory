// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypedFactoryRegistration.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Unity.AutoFactory.Implementation
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Implements the fluent interface for registering auto factories.
    /// </summary>
    /// <typeparam name="TFactory">
    /// The interface of the factory.
    /// </typeparam>
    /// <typeparam name="TFactoryImpl">
    /// The implementation of the factory (TODO: this should not be required, and should be automatically generated).
    /// </typeparam>
    internal class TypedFactoryRegistration<TFactory, TFactoryImpl> : ITypedFactoryRegistration
        where TFactoryImpl : class, TFactory
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedFactoryRegistration{TFactory, TFactoryImpl}"/> class.
        /// </summary>
        /// <param name="container">
        /// The target Unity container on which to perform the registrations.
        /// </param>
        /// <param name="lifetimeManager">
        /// The lifetime manager for the resulting auto factory.
        /// </param>
        public TypedFactoryRegistration(IUnityContainer container, LifetimeManager lifetimeManager)
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
        /// Gets the lifetime to be assigned to the <see cref="TFactory"/>.
        /// </summary>
        public LifetimeManager FactoryLifetimeManager { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Defines the concrete type which the factory will create.
        /// </summary>
        public void UsingConcreteType<TTo>()
        {
            this.Container.AddNewExtension<Interception>();

            this.Container.RegisterType<TFactory, TFactoryImpl>(
                this.FactoryLifetimeManager,
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior(new FactoryBehavior<TTo, TFactory>(this.Container)));
        }

        #endregion
    }
}