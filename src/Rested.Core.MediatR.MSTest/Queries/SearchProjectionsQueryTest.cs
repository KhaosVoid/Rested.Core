using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.Data.Projection;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.MSTest.Queries;

public abstract class SearchProjectionsQueryTest<TData, TDocument, TProjection, TSearchProjectionsQuery, TSearchProjectionsQueryValidator, TSearchProjectionsQueryHandler>
    where TData : IData
    where TDocument : IDocument<TData>
    where TProjection : Projection
    where TSearchProjectionsQuery : SearchProjectionsQuery<TData, TProjection>
    where TSearchProjectionsQueryValidator : SearchProjectionsQueryValidator<TData, TProjection, TSearchProjectionsQuery>
    where TSearchProjectionsQueryHandler : SearchProjectionsQueryHandler<TData, TProjection, TSearchProjectionsQuery>
{
    #region Constants

    protected const string TESTCATEGORY_QUERY_VALIDATOR_HANDLER_ERRORCODE_TESTS = "Query Validator & Handler Error Code Tests";
    protected const string TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS = "Query Validation Rule Tests";
    protected const string TESTCATEGORY_QUERY_TESTS = "Query Tests";

    #endregion Constants

    #region Properties

    public TestContext TestContext { get; set; }
    protected List<TDocument> TestDocuments { get; set; }
    protected List<TProjection> TestProjections { get; set; }

    #endregion Properties

    #region Members

    protected ILoggerFactory _loggerFactoryMock;

    protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";
    protected readonly string ASSERTMSG_ONLY_ONE_VALIDATION_ERROR = "only one validation error should occur for this test";
    protected readonly string ASSERTMSG_VALIDATION_ERROR_MESSAGE_SHOULD_MATCH = "error message should match";
    protected readonly string ASSERTMSG_VALIDATION_ERROR_CODE_SHOULD_MATCH = "error code should match";
    protected readonly string ASSERTMSG_QUERY_RESPONSE_SHOULD_NOT_BE_NULL = "the query response should not be null";

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

        TestContext.WriteLine("Initializing Projection Registration...");
        OnInitializeProjectionRegistration();

        TestContext.WriteLine("Initializing Test Projections...");
        OnInitializeTestProjections();
    }

    protected virtual void OnInitializeMockDependencies()
    {
        _loggerFactoryMock = Substitute.For<ILoggerFactory>();
    }

    protected abstract void OnInitializeTestDocuments();

    protected virtual void OnInitializeProjectionRegistration()
    {
        ProjectionRegistration.Initialize(typeof(TProjection).Assembly);
    }

    protected abstract void OnInitializeTestProjections();
    protected abstract List<TData> InitializeTestData();

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

    protected abstract TSearchProjectionsQuery CreateSearchProjectionsQuery(SearchRequest searchRequest);
    protected abstract TSearchProjectionsQueryValidator CreateSearchProjectionsQueryValidator();
    protected abstract TSearchProjectionsQueryHandler CreateSearchProjectionsQueryHandler();

    protected ValidationResult ExecuteQueryValidation(SearchRequest searchRequest) =>
        CreateSearchProjectionsQueryValidator().Validate(CreateSearchProjectionsQuery(searchRequest));

    protected void TestQueryValidationRule(SearchRequest searchRequest, ServiceErrorCode serviceErrorCode, params object[] messageFormatArgs)
    {
        var validationResult = ExecuteQueryValidation(searchRequest);

        validationResult.Errors.Count.Should().Be(
            expected: 1,
            because: ASSERTMSG_ONLY_ONE_VALIDATION_ERROR);

        validationResult.Errors.First().ErrorMessage.Should().Be(
            expected: string.Format(serviceErrorCode.Message, messageFormatArgs),
            because: ASSERTMSG_VALIDATION_ERROR_MESSAGE_SHOULD_MATCH);

        validationResult.Errors.First().ErrorCode.Should().Be(
            expected: serviceErrorCode.ExtendedStatusCode,
            because: ASSERTMSG_VALIDATION_ERROR_CODE_SHOULD_MATCH);
    }

    #endregion Methods

    #region Query Validator & Handler Error Code Tests

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATOR_HANDLER_ERRORCODE_TESTS)]
    public void IsQueryValidatorErrorCodesAssigned()
    {
        CreateSearchProjectionsQueryValidator().ServiceErrorCodes.Should().NotBeNull();
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATOR_HANDLER_ERRORCODE_TESTS)]
    public void IsQueryHandlerErrorCodesAssigned()
    {
        CreateSearchProjectionsQueryHandler().ServiceErrorCodes.Should().NotBeNull();
    }

    #endregion Query Validator & Handler Error Code Tests

    #region Query Validation Rule Tests

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
    public void PageMustBeGreaterThanZeroValidation()
    {
        TestQueryValidationRule(
            searchRequest: new SearchRequest()
            {
                Page = 0
            },
            serviceErrorCode: CreateSearchProjectionsQueryValidator().ServiceErrorCodes.CommonErrorCodes.PageMustBeGreaterThanZero);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
    public void PageInvalidValidation()
    {
        TestQueryValidationRule(
            searchRequest: new SearchRequest()
            {
                Page = 100000000,
                PageSize = 25
            },
            serviceErrorCode: CreateSearchProjectionsQueryValidator().ServiceErrorCodes.CommonErrorCodes.PageInvalid);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
    public void SortingFieldNameIsRequiredValidation()
    {
        TestQueryValidationRule(
            searchRequest: new SearchRequest()
            {
                Page = 1,
                SortingFields = new List<FieldSortInfo>()
                {
                    new FieldSortInfo()
                }
            },
            serviceErrorCode: CreateSearchProjectionsQueryValidator().ServiceErrorCodes.CommonErrorCodes.SortingFieldNameIsRequired);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
    public void SortingFieldNameIsInvalidValidation()
    {
        TestQueryValidationRule(
            searchRequest: new SearchRequest()
            {
                Page = 1,
                SortingFields = new List<FieldSortInfo>()
                {
                    new FieldSortInfo()
                    {
                        FieldName = "invalidField"
                    }
                }
            },
            serviceErrorCode: CreateSearchProjectionsQueryValidator().ServiceErrorCodes.CommonErrorCodes.SortingFieldNameIsInvalid);
    }

    #endregion Query Validation Rule Tests
}