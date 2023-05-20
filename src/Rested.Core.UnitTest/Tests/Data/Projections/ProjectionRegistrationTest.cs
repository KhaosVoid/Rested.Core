using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.CQRS.Data;
using Rested.Core.UnitTest.Data;

namespace Rested.Core.UnitTest.Tests.Data.Projections
{
    [TestClass]
    public class ProjectionRegistrationTest
    {
        #region Properties

        public TestContext TestContext { get; set; }

        #endregion Properties

        #region Members

        protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";

        #endregion Members

        #region Initialization

        [TestInitialize]
        public void Initialize()
        {
            TestContext.WriteLine(
                format: TESTCONTEXTMSG_TEST_STATUS,
                args: new[] { TestContext.TestName, TestContext.CurrentTestOutcome.ToString() });
            TestContext.WriteLine(string.Empty);

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
            TestContext.WriteLine("Initializing Projection Registration...");
            OnInitializeProjectionRegistration();
        }

        protected virtual void OnInitializeProjectionRegistration()
        {
            ProjectionRegistration.Initialize(typeof(EmployeeProjection).Assembly);
        }

        #endregion Initialization

        #region Test Cleanup

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine(
                format: TESTCONTEXTMSG_TEST_STATUS,
                args: new[] { TestContext.TestName, TestContext.CurrentTestOutcome.ToString() });
        }

        #endregion Test Cleanup

        #region Methods

        [TestMethod]
        public void CanInvokeStaticConstructorOnProjections()
        {
            ProjectionMappings
                .GetProjectionMappings<EmployeeProjection>()
                .Should()
                .NotBeNullOrEmpty();

            ProjectionMappings
                .GetProjectionMappings<TestDataProjection>()
                .Should()
                .NotBeNullOrEmpty();
        }

        #endregion Methods
    }
}
