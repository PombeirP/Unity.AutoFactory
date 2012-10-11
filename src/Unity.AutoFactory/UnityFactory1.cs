// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityFactory1.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory
{
    using System;

    /// <summary>
    /// Implementation of a Unity auto-factory which accepts a <typeparam name="TParam">parameter</typeparam> and returns a new <typeparam name="T">instance of T</typeparam>.
    /// </summary>
    /// <typeparam name="TParam">
    /// The type of the parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="T">
    /// The type returned by the factory.
    /// </typeparam>
    public class UnityFactory1<TParam, T> : IUnityFactory<TParam, T>
    {
        #region Fields

        /// <summary>
        /// The delayed-resolution lambda provided by Unity which can create an instance of <see cref="T"/>.
        /// </summary>
        private readonly Func<TParam, T> unityFactoryDelegate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityFactory1{TParam,T}"/> class.
        /// </summary>
        /// <param name="unityFactoryDelegate">
        /// The delayed-resolution lambda provided by Unity which can create an instance of <see cref="T"/>.
        /// </param>
        public UnityFactory1(Func<TParam, T> unityFactoryDelegate)
        {
            this.unityFactoryDelegate = unityFactoryDelegate;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates an instance of type <see cref="T"/> through the Unity container passing a parameter of type <see cref="TParam"/>.
        /// </summary>
        /// <param name="param">The parameter to pass to the constructor of <see cref="T">the resulting class</see>.</param>
        /// <returns>A new instance of <see cref="T"/>.</returns>
        public T Create(TParam param)
        {
            return this.unityFactoryDelegate(param);
        }

        #endregion
    }
}