// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnityFactory.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Unity.AutoFactory
{
    /// <summary>
    /// Defines the contract for a Unity auto-factory which returns a new <typeparam name="T">instance of T</typeparam>.
    /// </summary>
    /// <typeparam name="T">
    /// The type returned by the factory.
    /// </typeparam>
    public interface IUnityFactory<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates an instance of type <see cref="T"/> through the Unity container.
        /// </summary>
        /// <returns>A new instance of <see cref="T"/>.</returns>
        T Create();

        #endregion
    }

    /// <summary>
    /// Defines the contract for a Unity auto-factory which accepts a <typeparam name="TParam">parameter</typeparam> and returns a new <typeparam name="T">instance of T</typeparam>.
    /// </summary>
    /// <typeparam name="TParam">
    /// The type of the parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="T">
    /// The type returned by the factory.
    /// </typeparam>
    public interface IUnityFactory<TParam, T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates an instance of type <see cref="T"/> through the Unity container passing a parameter of type <see cref="TParam"/>.
        /// </summary>
        /// <param name="param">The parameter to pass to the constructor of <see cref="T">the resulting class</see>.</param>
        /// <returns>A new instance of <see cref="T"/>.</returns>
        T Create(TParam param);

        #endregion
    }

    /// <summary>
    /// Defines the contract for a Unity auto-factory which accepts a two parameters and returns a new <typeparam name="T">instance of T</typeparam>.
    /// </summary>
    /// <typeparam name="TParam1">
    /// The type of the first parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="TParam2">
    /// The type of the second parameter which will be passed to the <see cref="Create"/> method.
    /// </typeparam>
    /// <typeparam name="T">
    /// The type returned by the factory.
    /// </typeparam>
    public interface IUnityFactory<TParam1, TParam2, T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates an instance of type <see cref="T"/> through the Unity container passing two parameters of type <see cref="TParam1"/> and <see cref="TParam2"/>.
        /// </summary>
        /// <param name="param1">The first parameter to pass to the constructor of <see cref="T">the resulting class</see>.</param>
        /// <param name="param2">The second parameter to pass to the constructor of <see cref="T">the resulting class</see>.</param>
        /// <returns>A new instance of <see cref="T"/>.</returns>
        T Create(TParam1 param1, TParam2 param2);

        #endregion
    }
}