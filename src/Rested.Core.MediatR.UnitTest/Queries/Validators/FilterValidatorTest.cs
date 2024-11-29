using FluentAssertions;
using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.Data.Search;
using Rested.Core.Data.UnitTest.Data;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

public class FilterValidatorTest<TFilterValidator, TFilter>
    where TFilterValidator : AbstractValidator<TFilter>
    where TFilter : IFilter
{
    #region Constants

    protected const string TESTCATEGORY_FILTER_VALIDATOR_ERRORCODE_TESTS = "Filter Validator Error Code Tests";
    protected const string TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS = "Filter Validation Rule Tests";

    #endregion Constants
    
    #region Properties
    
    public TestContext TestContext { get; set; }
    protected List<IFilter> MockFilters { get; set; }
    protected ServiceErrorCodes ServiceErrorCodes { get; set; }
    
    #endregion Properties
    
    #region Members

    protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";
    protected readonly string ASSERTMSG_ONLY_ONE_VALIDATION_ERROR = "only one validation error should occur for this test";
    protected readonly string ASSERTMSG_VALIDATION_ERROR_MESSAGE_SHOULD_MATCH = "error message should match";
    protected readonly string ASSERTMSG_VALIDATION_ERROR_CODE_SHOULD_MATCH = "error code should match";

    #endregion Members
    
    #region Initialization

    [TestInitialize]
    public void Initialize()
    {
        TestContext.WriteLine(
            format: TESTCONTEXTMSG_TEST_STATUS,
            args: [TestContext.TestName, TestContext.CurrentTestOutcome.ToString()]);
        TestContext.WriteLine(string.Empty);

        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        TestContext.WriteLine("Initializing Test Service Error Codes...");
        OnInitializeServiceErrorCodes();
    }

    protected virtual void OnInitializeServiceErrorCodes()
    {
        ServiceErrorCodes = new ServiceErrorCodes(1, 2);
    }
    
    #endregion Initialization
    
    #region Test Cleanup

    [TestCleanup]
    public void TestCleanup()
    {
        TestContext.WriteLine(
            format: TESTCONTEXTMSG_TEST_STATUS,
            args: [TestContext.TestName, TestContext.CurrentTestOutcome.ToString()]);
    }
    
    #endregion Test Cleanup
    
    #region Methods

    protected TFilterValidator CreateValidator()
    {
        return (TFilterValidator)Activator.CreateInstance(
            type: typeof(TFilterValidator),
            args: [new ValidFieldNameGenerator(typeof(IDocument<Employee>)), ServiceErrorCodes]);
    }

    protected void TestFilterValidationRule(TFilterValidator filterValidator, TFilter filter, ServiceErrorCode serviceErrorCode, params object[] messageFormatArgs)
    {
        var validationResult = filterValidator.Validate(filter);

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
}