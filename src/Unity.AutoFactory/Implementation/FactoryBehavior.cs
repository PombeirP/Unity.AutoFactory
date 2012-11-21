namespace Unity.AutoFactory.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    internal class FactoryBehavior<TConcrete, TFactory> : IInterceptionBehavior
    {
        #region Constructors and Destructors

        public FactoryBehavior(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        #endregion

        #region Public Properties

        public bool WillExecute
        {
            get { return true; }
        }

        #endregion

        #region Properties

        private IUnityContainer Container { get; set; }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            yield return typeof(TFactory);
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var methodInfo = input.MethodBase as MethodInfo;

            if (methodInfo == null || !methodInfo.ReturnType.IsInterface)
            {
                var methodReturn = getNext();

                return methodReturn(input, getNext);
            }

            var returnType = methodInfo.ReturnType;
            object returnValue;
            object[] arguments;

            if (!returnType.IsAssignableFrom(typeof(TConcrete)))
            {
                throw new InvalidCastException(string.Format("The concrete type {0} does not implement the factory return type {1}", typeof(TConcrete).FullName, returnType.FullName));
            }

            if (input.Arguments.Count == 0)
            {
                returnValue = this.Container.Resolve(typeof(TConcrete));
                arguments = new object[0];
            }
            else
            {
                returnValue = this.Container.Resolve(typeof(TConcrete), GetDependencyOverrides(input.Arguments).ToArray());
                arguments = input.Arguments.Cast<object>().ToArray();
            }

            return new VirtualMethodReturn(input, returnValue, arguments);
        }

        #endregion

        #region Methods

        private static IEnumerable<ResolverOverride> GetDependencyOverrides(IParameterCollection arguments)
        {
            for (var parameterIndex = 0; parameterIndex < arguments.Count; ++parameterIndex)
            {
                var parameterInfo = arguments.GetParameterInfo(parameterIndex);
                var parameterValue = arguments[parameterIndex];

                yield return new DependencyOverride(parameterInfo.ParameterType, new InjectionParameter(parameterInfo.ParameterType, parameterValue));
            }
        }

        #endregion
    }
}