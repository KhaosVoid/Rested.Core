using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Data.Document;
using Rested.Core.Data.Projection;
using Rested.Core.Data.UnitTest.Data;

namespace Rested.Core.Data.UnitTest.Tests.Data.Projections;

[TestClass]
public class ProjectionMappingsTest
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

    protected virtual void OnInitialize() { }

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
    public void CanRegister()
    {
        ProjectionMappings.Register((TestDataProjection p) => p.Property1, (IDocument<TestData> d) => d.Data.Property1);

        ProjectionMappings.TryGetMapping((TestDataProjection p) => p.Property1, out var projectionMapping);

        projectionMapping.Should().NotBeNull();
    }

    [TestMethod]
    public void TestDuplicateProjectionMappingExceptionOnRegister()
    {
        try
        {
            ProjectionMappings.Register((TestDataProjection p) => p.Property2, (IDocument<TestData> d) => d.Data.Property2);
            ProjectionMappings.Register((TestDataProjection p) => p.Property2, (IDocument<TestData> d) => d.Data.Property2);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<DuplicateProjectionMappingException>();
        }
    }

    [TestMethod]
    public void TestProjectionMappingNotRegisteredExceptionOnTryGetMapping()
    {
        try
        {
            ProjectionMappings.TryGetMapping((TestDataProjection p) => p.Property3, out var _);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ProjectionMappingNotRegisteredException>();
        }
    }

    [TestMethod]
    public void TestProjectionMappingNotRegisteredExceptionOnGetProjectionMappings()
    {
        try
        {
            ProjectionMappings.GetProjectionMappings<TestDataProjection2>();
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ProjectionMappingNotRegisteredException>();
        }
    }

    #endregion Methods
}