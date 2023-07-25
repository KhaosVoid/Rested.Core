using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.Data;
using Rested.Core.MediatR.Commands;
using Rested.Core.MediatR.Queries;
using Rested.Core.Server.Controllers;
using Rested.Core.Server.Http;
using Rested.Core.Server.MSTest;
using System.Reflection;

namespace Rested.Core.Server.MSTest.Controllers
{
    public abstract class DocumentControllerTest<TData, TDocument, TDocumentController>
        where TData : IData
        where TDocument : IDocument<TData>
        where TDocumentController : DocumentController<TData>
    {
        #region Constants

        protected const string TESTCATEGORY_CONTROLLER_TESTS = "Controller Tests";

        #endregion Constants

        #region Properties

        public TestContext TestContext { get; set; }
        protected List<Dto<TData>> TestDtos { get; set; }
        protected List<TDocument> TestDocuments { get; set; }

        #endregion Properties

        #region Members

        protected IMediator _mediatorMock;
        protected IHttpContextAccessor _httpContextMock;
        protected ILoggerFactory _loggerFactoryMock;

        protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";
        protected readonly string ASSERTMSG_CONTROLLER_METHOD_MUST_HAVE_HTTPMETHODATTRIBUTE = $"the {typeof(TDocumentController).Name}.{{0}} method must have the {{1}}";
        protected readonly string ASSERTMSG_CONTROLLER_METHOD_IGNORED =
            $"The {{0}} test has been skipped because the {typeof(TDocumentController).Name}.{{1}} method has been marked " +
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
        }

        protected virtual void OnInitializeMockDependencies()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _httpContextMock = Substitute.For<IHttpContextAccessor>();
            _loggerFactoryMock = Substitute.For<ILoggerFactory>();
        }

        protected virtual void OnInitializeTestDocuments()
        {
            TestContext.WriteLine("Initializing Test Dtos...");
            OnInitializeTestDtos();

            TestDocuments = TestDtos.Select(
                dto =>
                {
                    var document = CreateDocument(dto.Data);

                    document.Id = dto.Id;
                    document.ETag = dto.ETag;

                    return document;
                }).ToList();
        }

        protected virtual void OnInitializeTestDtos()
        {
            TestDtos = InitializeTestData().Select(CreateDto).ToList();
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

        protected Dto<TData> CreateDto(TData data = default) => CreateDto<TData>(data);

        protected Dto<T> CreateDto<T>(T data = default) where T : IData
        {
            return new Dto<T>()
            {
                Id = Guid.NewGuid(),
                ETag = BitConverter.GetBytes(0UL),
                Data = data
            };
        }

        protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
        protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

        protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
        protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

        protected virtual TDocumentController CreateController()
        {
            return (TDocumentController)Activator.CreateInstance(
                type: typeof(TDocumentController),
                args: new object[] { _mediatorMock, _httpContextMock, _loggerFactoryMock });
        }

        protected bool IsApiExplorerSettingsIgnored(string methodName)
        {
            var apiExplorerSettingsAttribute = typeof(TDocumentController)
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
        public void GetDocument()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(GetDocument),
                controllerMethodName: nameof(DocumentController<TData>.GetDocument));

            _mediatorMock
                .Send(Arg.Any<GetDocumentQuery<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments.First());

            var response = CreateController()
                .GetDocument(id: TestDocuments.First().Id)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<TDocument>().Should().BeEquivalentTo(TestDocuments.First());
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void GetDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(GetDocuments),
                controllerMethodName: nameof(DocumentController<TData>.GetDocuments));

            _mediatorMock
                .Send(Arg.Any<GetDocumentsQuery<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments);

            var response = CreateController()
                .GetDocuments()
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<List<TDocument>>().Should().BeEquivalentTo(TestDocuments);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void SearchDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(SearchDocuments),
                controllerMethodName: nameof(DocumentController<TData>.SearchDocuments));

            var searchRequest = new SearchRequest()
            {
                Page = 1,
                PageSize = 25
            };

            var searchDocumentsResults = new SearchDocumentsResults<TData, TDocument>(searchRequest)
            {
                TotalPages = 1,
                TotalQueriedRecords = TestDocuments.Count,
                TotalRecords = TestDocuments.Count,
                Items = TestDocuments
            };

            _mediatorMock
                .Send(Arg.Any<SearchDocumentsQuery<TData, TDocument>>())
                .ReturnsForAnyArgs(searchDocumentsResults);

            var response = CreateController()
                .SearchDocuments(searchRequest)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<SearchDocumentsResults<TData, TDocument>>().Should().BeEquivalentTo(searchDocumentsResults);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void InsertDocument()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(InsertDocument),
                controllerMethodName: nameof(DocumentController<TData>.InsertDocument));

            _mediatorMock
                .Send(Arg.Any<DocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments.First());

            var response = CreateController()
                .InsertDocument(data: TestDtos.First().Data)
                .Result
                .Result as CreatedResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Location.Should().Be($"{_httpContextMock.HttpContext.Request.Path}/{TestDocuments.First().Id}");
            response.Value.As<TDocument>().Should().BeEquivalentTo(TestDocuments.First());
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void InsertMultipleDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(InsertMultipleDocuments),
                controllerMethodName: nameof(DocumentController<TData>.InsertMultipleDocuments));

            _mediatorMock
                .Send(Arg.Any<MultiDocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments);

            var response = CreateController()
                .InsertMultipleDocuments(datas: TestDtos.Select(dto => dto.Data).ToList())
                .Result
                .Result as CreatedResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Location.Should().Be($"{_httpContextMock.HttpContext.Request.Path}/{TestDocuments.First().Id}");
            response.Value.As<List<TDocument>>().Should().BeEquivalentTo(TestDocuments);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void UpdateDocument()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(UpdateDocument),
                controllerMethodName: nameof(DocumentController<TData>.UpdateDocument));

            _mediatorMock
                .Send(Arg.Any<DocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments.First());

            var response = CreateController()
                .UpdateDocument(
                    id: TestDocuments.First().Id,
                    etag: new IfMatchByteArray(TestDocuments.First().ETag),
                    data: TestDocuments.First().Data)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<TDocument>().Should().BeEquivalentTo(TestDocuments.First());
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void UpdateMultipleDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(UpdateMultipleDocuments),
                controllerMethodName: nameof(DocumentController<TData>.UpdateMultipleDocuments));

            _mediatorMock
                .Send(Arg.Any<MultiDocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments);

            var response = CreateController()
                .UpdateMultipleDocuments(TestDtos)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<List<TDocument>>().Should().BeEquivalentTo(TestDocuments);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void PatchDocument()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(PatchDocument),
                controllerMethodName: nameof(DocumentController<TData>.PatchDocument));

            _mediatorMock
                .Send(Arg.Any<DocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments.First());

            var response = CreateController()
                .PatchDocument(
                    id: TestDocuments.First().Id,
                    etag: new IfMatchByteArray(TestDocuments.First().ETag),
                    data: TestDocuments.First().Data)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<TDocument>().Should().BeEquivalentTo(TestDocuments.First());
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void PatchMultipleDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(PatchMultipleDocuments),
                controllerMethodName: nameof(DocumentController<TData>.PatchMultipleDocuments));

            _mediatorMock
                .Send(Arg.Any<MultiDocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(TestDocuments);

            var response = CreateController()
                .PatchMultipleDocuments(TestDtos)
                .Result
                .Result as OkObjectResult;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.As<List<TDocument>>().Should().BeEquivalentTo(TestDocuments);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void DeleteDocument()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(DeleteDocument),
                controllerMethodName: nameof(DocumentController<TData>.DeleteDocument));

            _mediatorMock
                .Send(Arg.Any<DocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(Task.FromResult<TDocument>(default));

            var response = CreateController()
                .DeleteDocument(
                    id: TestDocuments.First().Id,
                    etag: new IfMatchByteArray(TestDocuments.First().ETag))
                .Result;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_CONTROLLER_TESTS)]
        public void DeleteMultipleDocuments()
        {
            ShouldControllerTestBeSkipped(
                testMethodName: nameof(DeleteMultipleDocuments),
                controllerMethodName: nameof(DocumentController<TData>.DeleteMultipleDocuments));

            _mediatorMock
                .Send(Arg.Any<MultiDocumentCommand<TData, TDocument>>())
                .ReturnsForAnyArgs(Task.FromResult<List<TDocument>>(default));

            var response = CreateController()
                .DeleteMultipleDocuments(BaseDto.ToList(TestDtos))
                .Result;

            response.Should().NotBeNull(because: ASSERTMSG_RESPONSE_SHOULD_NOT_BE_NULL);
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        #endregion Controller Tests
    }
}
