// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityAutoFactoryTests.cs" company="Pedro Pombeiro">
//   2012 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Unity.AutoFactory.Tests
// ReSharper disable InconsistentNaming
{
    using Microsoft.Practices.Unity;

    using NUnit.Framework;

    [TestFixture]
    public class UnityAutoFactoryTests
    {
        #region Interfaces

        private interface ISomeInstance
        {
        }

        private interface ISomeService
        {
        }

        /// <summary>
        /// The Test interface for one parameter.
        /// </summary>
        private interface ITest1
        {
            #region Public Properties

            string TestProperty1 { get; }

            #endregion
        }

        /// <summary>
        /// The Test interface for two parameters.
        /// </summary>
        private interface ITest2
        {
            #region Public Properties

            ISomeService InjectedService { get; }

            string TestProperty1 { get; }

            ISomeInstance TestProperty2 { get; }

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_one_parameter_then_TestProperty1_on_resulting_TestClass_matches_specified_value()
        {
            // Arrange
            var unityContainer = new UnityContainer();

            unityContainer.RegisterAutoFactoryFor<ITest1, TestClass1>().WithParam<string>();

            const string TestValue = "TestValue";

            // Act
            var factory = unityContainer.Resolve<IUnityFactory<string, ITest1>>();
            var testClass = factory.Create(TestValue);

            // Assert
            Assert.AreEqual(TestValue, testClass.TestProperty1);
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_then_TestProperty1_and_2_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer.RegisterAutoFactoryFor<ITest2, TestClass2>().WithParams<string, ISomeInstance>();

                const string TestValue = "TestValue";
                ISomeInstance someInstance = new SomeInstance();

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<IUnityFactory<string, ISomeInstance, ITest2>>();
                var testClass = factory.Create(TestValue, someInstance);

                // Assert
                Assert.AreEqual(TestValue, testClass.TestProperty1);
                Assert.AreEqual(someInstance, testClass.TestProperty2);
                Assert.AreEqual(unityContainer.Resolve<ISomeService>(), testClass.InjectedService);
            }
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_and_parameter_2_is_null_then_TestProperty1_and_2_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer.RegisterAutoFactoryFor<ITest2, TestClass2>().WithParams<string, ISomeInstance>();

                const string TestValue = "TestValue";
                ISomeInstance someInstance = null;

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<IUnityFactory<string, ISomeInstance, ITest2>>();
                var testClass = factory.Create(TestValue, someInstance);

                // Assert
                Assert.AreEqual(TestValue, testClass.TestProperty1);
                Assert.AreEqual(someInstance, testClass.TestProperty2);
                Assert.AreEqual(unityContainer.Resolve<ISomeService>(), testClass.InjectedService);
            }
        }

        [Test]
        public void given_instantiated_Sut_when_Create_is_called_with_two_parameters_and_parameter_1_is_null_then_TestProperty1_and_2_on_resulting_TestClass_matches_specified_values()
        {
            // Arrange
            using (var unityContainer = new UnityContainer())
            {
                unityContainer.RegisterAutoFactoryFor<ITest2, TestClass2>().WithParams<string, ISomeInstance>();

                const string TestValue =null;
                ISomeInstance someInstance = new SomeInstance();

                unityContainer.RegisterType<ISomeService, SomeService>(new ContainerControlledLifetimeManager());

                // Act
                var factory = unityContainer.Resolve<IUnityFactory<string, ISomeInstance, ITest2>>();
                var testClass = factory.Create(TestValue, someInstance);

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

            unityContainer.RegisterAutoFactoryFor<ISomeInstance, SomeInstance>().WithoutParameters();

            // Act
            var factory = unityContainer.Resolve<IUnityFactory<ISomeInstance>>();
            var testClass = factory.Create();

            // Assert
            Assert.IsInstanceOf<SomeInstance>(testClass);
        }

        #endregion

        private class SomeInstance : ISomeInstance
        {
        }

        private class SomeService : ISomeService
        {
        }

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

        private class TestClass2 : ITest2
        {
            #region Constructors and Destructors

            public TestClass2(ISomeInstance testProperty2, string testProperty1, ISomeService someService)
            {
                this.InjectedService = someService;
                this.TestProperty1 = testProperty1;
                this.TestProperty2 = testProperty2;
            }

            #endregion

            #region Public Properties

            public ISomeService InjectedService { get; private set; }

            public string TestProperty1 { get; private set; }

            public ISomeInstance TestProperty2 { get; private set; }

            #endregion
        }
    }
}