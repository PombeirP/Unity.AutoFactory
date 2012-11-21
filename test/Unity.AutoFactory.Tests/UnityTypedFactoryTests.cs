// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityTypedFactoryTests.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Unity.AutoFactory.Tests
// ReSharper disable InconsistentNaming
{
    using JetBrains.Annotations;

    using Microsoft.Practices.Unity;

    using NUnit.Framework;

    [TestFixture]
    public class UnityTypedFactoryTests
    {
        #region Interfaces

        public interface ISomeInstance
        {
        }

        public interface ISomeInstanceFactory
        {
            #region Public Methods and Operators

            ISomeInstance Create();

            #endregion
        }

        public interface ISomeService
        {
        }

        /// <summary>
        /// The Test interface for one parameter.
        /// </summary>
        public interface ITest1
        {
            #region Public Properties

            string TestProperty1 { get; }

            #endregion
        }

        //[Factory]
        public interface ITest1Factory
        {
            #region Public Methods and Operators

            ITest1 Create(string textParam);

            #endregion
        }

        /// <summary>
        /// The Test interface for two parameters.
        /// </summary>
        public interface ITest2
        {
            #region Public Properties

            ISomeService InjectedService { get; }

            string TestProperty1 { get; }

            ISomeInstance TestProperty2 { get; }

            string TestProperty3 { get; }

            #endregion
        }

        public interface ITest2Factory
        {
            #region Public Methods and Operators

            ITest2 Create(string testProperty1, ISomeInstance testProperty2, string testProperty3);

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_one_parameter_then_TestProperty1_on_resulting_TestClass_matches_specified_value()
        {
            // Arrange
            var unityContainer = new UnityContainer();

            unityContainer
                .RegisterAutoFactory<ITest1Factory, Test1Factory>()
                .UsingConcreteType<TestClass1>();

            const string TestValue = "TestValue";

            // Act
            var factory = unityContainer.Resolve<ITest1Factory>();
            var testClass = factory.Create(TestValue);

            // Assert
            Assert.AreEqual(TestValue, testClass.TestProperty1);
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_and_parameter_1_is_null_then_TestProperty1_and_2_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer
                    .RegisterAutoFactory<ITest2Factory, TestClass2Factory>()
                    .UsingConcreteType<TestClass2>();

                const string TestValue = null;
                ISomeInstance someInstance = new SomeInstance();

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<ITest2Factory>();
                var testClass = factory.Create(TestValue, someInstance, string.Empty);

                // Assert
                Assert.AreEqual(TestValue, testClass.TestProperty1);
                Assert.AreEqual(someInstance, testClass.TestProperty2);
                Assert.AreEqual(unityContainer.Resolve<ISomeService>(), testClass.InjectedService);
                Assert.AreEqual(string.Empty, testClass.TestProperty3);
            }
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_and_parameter_2_is_null_then_TestProperty1_and_2_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer
                    .RegisterAutoFactory<ITest2Factory, TestClass2Factory>()
                    .UsingConcreteType<TestClass2>();

                const string TestValue = "TestValue";
                ISomeInstance someInstance = null;

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<ITest2Factory>();
                var testClass = factory.Create(string.Empty, someInstance, TestValue);

                // Assert
                Assert.AreEqual(string.Empty, testClass.TestProperty1);
                Assert.AreEqual(TestValue, testClass.TestProperty2);
                Assert.AreEqual(someInstance, testClass.TestProperty2);
                Assert.AreEqual(unityContainer.Resolve<ISomeService>(), testClass.InjectedService);
            }
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_then_TestProperty1_2_and_3_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer
                    .RegisterAutoFactory<ITest2Factory, TestClass2Factory>()
                    .UsingConcreteType<TestClass2>();

                const string TestValue = "TestValue";
                ISomeInstance someInstance = new SomeInstance();

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<ITest2Factory>();
                var testClass = factory.Create(TestValue, someInstance, string.Empty);

                // Assert
                Assert.AreEqual(TestValue, testClass.TestProperty1);
                Assert.AreEqual(someInstance, testClass.TestProperty2);
                Assert.AreEqual(unityContainer.Resolve<ISomeService>(), testClass.InjectedService);
            }
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_without_parameters_then_valid_instance_is_returned()
        {
            // Arrange
            var unityContainer = new UnityContainer();

            unityContainer
                .RegisterAutoFactory<ISomeInstanceFactory, SomeInstanceFactory>()
                .UsingConcreteType<SomeInstance>();

            // Act
            var factory = unityContainer.Resolve<ISomeInstanceFactory>();
            var testClass = factory.Create();

            // Assert
            Assert.IsInstanceOf<SomeInstance>(testClass);
        }

        #endregion

        public class TestClass2Factory : ITest2Factory
        {
            #region Public Methods and Operators

            public ITest2 Create(string testProperty1, ISomeInstance testProperty2, string testProperty3)
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }

        internal class Test1Factory : ITest1Factory
        {
            #region Explicit Interface Methods

            ITest1 ITest1Factory.Create(string textParam)
            {
                return null;
            }

            #endregion
        }

        [UsedImplicitly]
        private class SomeInstance : ISomeInstance
        {
        }

        [UsedImplicitly]
        private class SomeInstanceFactory : ISomeInstanceFactory
        {
            #region Public Methods and Operators

            public ISomeInstance Create()
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }

        [UsedImplicitly]
        private class SomeService : ISomeService
        {
        }

        [UsedImplicitly]
        private class TestClass1 : ITest1
        {
            #region Constructors and Destructors

            public TestClass1(string testProperty)
            {
                this.TestProperty1 = testProperty;
            }

            #endregion

            #region Public Properties

            public string TestProperty1 { get; private set; }

            #endregion
        }

        [UsedImplicitly]
        private class TestClass2 : ITest2
        {
            #region Constructors and Destructors

            public TestClass2(ISomeInstance testProperty2, string testProperty1, ISomeService someService, string testProperty3)
            {
                this.InjectedService = someService;
                this.TestProperty3 = testProperty3;
                this.TestProperty1 = testProperty1;
                this.TestProperty2 = testProperty2;
            }

            #endregion

            #region Public Properties

            public ISomeService InjectedService { get; private set; }

            public string TestProperty1 { get; private set; }

            public ISomeInstance TestProperty2 { get; private set; }

            public string TestProperty3 { get; private set; }

            #endregion
        }
    }
}