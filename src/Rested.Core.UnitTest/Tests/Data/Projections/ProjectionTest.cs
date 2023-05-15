using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.CQRS.Data;
using Rested.Core.UnitTest.Data;

namespace Rested.Core.UnitTest.Tests.Data.Projections
{
    [TestClass]
    public class ProjectionTest
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
        public void CanGetProjectionExpression()
        {
            ProjectionTestingUtils.InitilizeProjectionRegistration();

            var projectionExpression = Projection.GetProjectionExpression<EmployeeProjection, IDocument<Employee>>();
            var employeeDocumentMock = NSubstitute.Substitute.For<IDocument<Employee>>();

            employeeDocumentMock.Id = Guid.NewGuid();
            employeeDocumentMock.Data = new Employee()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                FullName = "FirstName LastName",
                Age = 30,
                DOB = DateTime.Now,
                StartDate = DateTime.Now,
                EmploymentType = EmploymentTypes.Contract
            };

            var projection = projectionExpression.Compile().Invoke(employeeDocumentMock);

            projection.Should().NotBeNull();
            projection.Id.Should().Be(employeeDocumentMock.Id);
            projection.FullName.Should().Be(employeeDocumentMock.Data.FullName);
            projection.Age.Should().Be(employeeDocumentMock.Data.Age);
            projection.DOB.Should().Be(employeeDocumentMock.Data.DOB);
            projection.StartDate.Should().Be(employeeDocumentMock.Data.StartDate);
            projection.EmploymentType.Should().Be(employeeDocumentMock.Data.EmploymentType);
        }

        #endregion Methods
    }
}
