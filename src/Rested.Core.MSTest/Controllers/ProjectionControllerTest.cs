using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.Controllers;
using Rested.Core.CQRS.Data;
using Rested.Core.CQRS.Queries;
using System.Reflection;

namespace Rested.Core.MSTest.Controllers
{
    public abstract class ProjectionControllerTest<TData, TDocument, TProjection, TProjectionController>
        where TData : IData
        where TDocument : IDocument<TData>
        where TProjection : Projection
        where TProjectionController : ProjectionController<TData, TProjection>
    {
        #region Constants

        protected const string TESTCATEGORY_CONTROLLER_TESTS = "Controller Tests";

        #endregion Constants

        #region Properties

        public TestContext TestContext { get; set; }
        public List<TDocument> TestDocuments { get; set; }
        public List<TProjection> TestProjections { get; set; }

        #endregion Properties

        #region Members

        protected IMediator _mediatorMock;
        protected IHttpContextAccessor _httpContextMock;
        protected ILoggerFactory _loggerFactoryMock;

        protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";
        protected readonly string ASSERTMSG_CONTROLLER_METHOD_MUST_HAVE_HTTPMETHODATTRIBUTE = $"the {typeof(TProjectionController).Name}.{{0}} method must have the {{1}}";
        protected readonly string ASSERTMSG_CONTROLLER_METHOD_IGNORED =
            $"The {{0}} test has been skipped because the {typeof(TProjectionController).Name}.{{1}} method has been marked " +
            $"with the {nameof(ApiExplorerSettingsAttribute)} and the {nameof(ApiExplorerSettingsAttribute.IgnoreApi)} property has been set to true";
        protected readonly string ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL = $"the response should not be null";
        protected readonly string ASSERTMSG_RESPONSE_SHOULD_BE_NULL = $"the response should be null";

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
            TestContext.WriteLine("Initializing Mock Dependencies...");
            OnInitializeMockDependencies();

            TestContext.WriteLine("Initializing Test Documents...");
            OnInitializeTestDocuments();

            TestContext.WriteLine("Registering Projection Mappings...");
            RegisterProjectionMappings();

            TestContext.WriteLine("Initializing Test Projections...");
            OnInitializeTestProjections();
        }

        protected virtual void OnInitializeMockDependencies()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _httpContextMock = Substitute.For<IHttpContextAccessor>();
            _loggerFactoryMock = Substitute.For<ILoggerFactory>();
        }

        protected virtual void OnInitializeTestDocuments()
        {
            TestDocuments = InitializeTestData().Select(CreateDocument).ToList();
        }

        private void RegisterProjectionMappings()
        {
            _ = new ProjectionRegistration();
        }

        protected virtual void OnInitializeTestProjections()
        {
            TestProjections = TestDocuments.Select(CreateProjection).ToList();
        }

        protected abstract List<TData> InitializeTestData();

        #endregion Initialization

        #region Test Cleanup

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine(string.Empty);
            TestContext.WriteLine(
                format: TESTCONTEXTMSG_TEST_STATUS,
                args: new[] { TestContext.TestName, TestContext.CurrentTestOutcome.ToString() });
        }

        #endregion Test Cleanup

        #region Methods

        protected virtual TProjection CreateProjection(TDocument document)
        {
            return Projection
                .GetProjectionExpression<TProjection, TDocument>()
                .Compile()
                .Invoke(document);
        }

        protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
        protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

        protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
        protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

        protected virtual TProjectionController CreateController()
        {
            return (TProjectionController)Activator.CreateInstance(
                type: typeof(TProjectionController),
                args: new object[] { _mediatorMock, _httpContextMock, _loggerFactoryMock });
        }

        protected bool IsApiExplorerSettingsIgnored(string methodName)
        {
            var apiExplorerSettingsAttribute = typeof(TProjectionController)
                .GetMethod(
                    name: methodName,
                    bindingAttr: BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)
                .GetCustomAttribute<ApiExplorerSettingsAttribute>();

            if (apiExplorerSettingsAttribute is not null)
                return apiExplorerSettingsAttribute.IgnoreApi;

            return false;
        }

        protected void ShouldControllerTestBeSkipped(string testMethodName, string controllerMethodName)
        {
            if (IsApiExplorerSettingsIgnored(controllerMethodName))
                Assert.Inconclusive(
                    message: ASSERTMSG_CONTROLLER_METHOD_IGNORED,
                    parameters: new[] { testMethodName, controllerMethodName });
        }

        #endregion Methods

        #region Controller Tests

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void GetProjection()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(GetProjection),
                controllerMethodName: nameof(ProjectionController<TData, TProjection>.GetProjection));

            _mediatorMock
                .Send(Arg.Any<GetProjectionQuery<TData, TProjection>>())
                .ReturnsForAnyArgs(TestProjections.First());

            var response = CreateController()
                .GetProjection(id: TestDocuments.First().Id)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<TProjection>().Should().BeEquivalentTo(TestProjections.First());
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void GetProjections()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(GetProjections),
                controllerMethodName: nameof(ProjectionController<TData, TProjection>.GetProjections));

            _mediatorMock
                .Send(Arg.Any<GetProjectionsQuery<TData, TProjection>>())
                .ReturnsForAnyArgs(TestProjections);

            var response = CreateController()
                .GetProjections()
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<List<TProjection>>().Should().BeEquivalentTo(TestProjections);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void SearchProjections()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(SearchProjections),
                controllerMethodName: nameof(ProjectionController<TData, TProjection>.SearchProjections));

            var searchRequest = new SearchRequest()
            {
                Page = 1,
                PageSize = 25
            };

            var searchProjectionsResults = new SearchProjectionsResults<TData, TProjection>(searchRequest)
            {
                TotalPages = 1,
                TotalQueriedRecords = TestProjections.Count,
                TotalRecords = TestProjections.Count,
                Items = TestProjections
            };

            _mediatorMock
                .Send(Arg.Any<SearchProjectionsQuery<TData, TProjection>>())
                .ReturnsForAnyArgs(searchProjectionsResults);

            var response = CreateController()
                .SearchProjections(searchRequest)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<SearchProjectionsResults<TData, TProjection>>().Should().BeEquivalentTo(searchProjectionsResults);
        }

        #endregion Controller Tests
    }
}
