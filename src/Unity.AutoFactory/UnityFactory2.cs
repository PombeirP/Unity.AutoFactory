// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityFactory2.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory
{
    using System;

    /// <summary>
    /// Defines the contract for a Unity auto-factory which accepts a two parameters and returns a new .
    /// </summary>
    /// <typeparam name="TParam1">
    /// The type of the first parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="TParam2">
    /// The type of the second parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="T">
    /// instance of T
    /// </typeparam>
    /// <typeparam name="T">
    /// The type returned by the factory.
    /// </typeparam>
    public class UnityFactory2<TParam1, TParam2, T> :
        IUnityFactory<TParam1, TParam2, T>
    {
        #region Fields

        /// <summary>
        /// The delayed-resolution lambda provided by Unity which can create an instance of <see cref="T"/>.
        /// </summary>
        private readonly Func<TParam1, TParam2, T> unityFactoryDelegate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityFactory2{TParam1,TParam2,T}"/> class. 
        /// Initializes a new instance of the <see cref="UnityFactory0{TParam1,TParam2,T}"/> class.
        /// </summary>
        /// <param name="unityFactoryDelegate">
        /// The delayed-resolution lambda provided by Unity which can create an instance of <see cref="T"/>.
        /// </param>
        public UnityFactory2(Func<TParam1, TParam2, T> unityFactoryDelegate)
        {
            this.unityFactoryDelegate = unityFactoryDelegate;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates an instance of type <see cref="T"/> through the Unity container passing two parameters of type <see cref="TParam1"/> and <see cref="TParam2"/>.
        /// </summary>
        /// <param name="param1">
        /// The first parameter to pass to the constructor of <see cref="T">the resulting class</see>.
        /// </param>
        /// <param name="param2">
        /// The second parameter to pass to the constructor of <see cref="T">the resulting class</see>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="T"/>.
        /// </returns>
        public T Create(TParam1 param1, TParam2 param2)
        {
            return this.unityFactoryDelegate(param1, param2);
        }

        #endregion
    }
}