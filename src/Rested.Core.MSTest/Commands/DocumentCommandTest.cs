using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.Commands;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.MSTest.Commands
{
    public abstract class DocumentCommandTest<TData, TDocument, TDocumentCommand, TDocumentCommandValidator, TDocumentCommandHandler>
        where TData : IData
        where TDocument : IDocument<TData>
        where TDocumentCommand : DocumentCommand<TData, TDocument>
        where TDocumentCommandValidator : DocumentCommandValidator<TData, TDocument, TDocumentCommand>
        where TDocumentCommandHandler : DocumentCommandHandler<TData, TDocument, TDocumentCommand>
    {
        #region Constants

        protected const string TESTCATEGORY_COMMAND_VALIDATOR_HANDLER_ERRORCODE_TESTS = "Command Validator & Handler Error Code Tests";
        protected const string TESTCATEGORY_INSERT_VALIDATION_RULE_TESTS = "Insert Validation Rule Tests";
        protected const string TESTCATEGORY_UPDATE_VALIDATION_RULE_TESTS = "Update Validation Rule Tests";
        protected const string TESTCATEGORY_PATCH_VALIDATION_RULE_TESTS = "Patch Validation Rule Tests";
        protected const string TESTCATEGORY_DELETE_VALIDATION_RULE_TESTS = "Delete Validation Rule Tests";
        protected const string TESTCATEGORY_INSERT_TESTS = "Insert Tests";
        protected const string TESTCATEGORY_UPDATE_TESTS = "Update Tests";
        protected const string TESTCATEGORY_PATCH_TESTS = "Patch Tests";
        protected const string TESTCATEGORY_DELETE_TESTS = "Delete Tests";

        #endregion Constants

        #region Properties

        public TestContext TestContext { get; set; }
        protected TDocument TestDocument { get; set; }

        #endregion Properties

        #region Members

        protected ILoggerFactory _loggerFactoryMock;

        protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";
        protected readonly string ASSERTMSG_ONLY_ONE_VALIDATION_ERROR = "only one validation error should occur for this test";
        protected readonly string ASSERTMSG_VALIDATION_ERROR_MESSAGE_SHOULD_MATCH = "the validation error message should match";
        protected readonly string ASSERTMSG_VALIDATION_ERROR_CODE_SHOULD_MATCH = "the validation error code should match";
        protected readonly string ASSERTMSG_DOCUMENT_COMMAND_RESPONSE_SHOULD_NOT_BE_NULL = $"the {typeof(TDocumentCommand).Name} response should not be null";
        protected readonly string ASSERTMSG_DOCUMENT_COMMAND_RESPONSE_SHOULD_BE_NULL = $"the {typeof(TDocumentCommand).Name} response should be null";

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

            TestContext.WriteLine("Initializing Test Document...");
            OnInitializeTestDocument();
        }

        protected virtual void OnInitializeMockDependencies()
        {
            _loggerFactoryMock = Substitute.For<ILoggerFactory>();
        }

        protected abstract void OnInitializeTestDocument();
        protected abstract TData InitializeTestData();

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

        protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
        protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

        protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
        protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

        protected abstract TDocumentCommand CreateDocumentCommand(CommandActions action);
        protected abstract TDocumentCommandValidator CreateDocumentCommandValidator();
        protected abstract TDocumentCommandHandler CreateDocumentCommandHandler();

        protected ValidationResult ExecuteCommandValidation(CommandActions action) =>
            CreateDocumentCommandValidator().Validate(CreateDocumentCommand(action));

        protected void TestCommandValidationRule(CommandActions action, ServiceErrorCode serviceErrorCode, params object[] messageFormatArgs)
        {
            var validationResult = ExecuteCommandValidation(action);

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

        #region Command Validator & Handler Error Code Tests

        [TestMethod]
        [TestCategory(TESTCATEGORY_COMMAND_VALIDATOR_HANDLER_ERRORCODE_TESTS)]
        public void IsCommandValidatorErrorCodesAssigned()
        {
            CreateDocumentCommandValidator().ServiceErrorCodes.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_COMMAND_VALIDATOR_HANDLER_ERRORCODE_TESTS)]
        public void IsCommandHandlerErrorCodesAssigned()
        {
            CreateDocumentCommandHandler().ServiceErrorCodes.Should().NotBeNull();
        }

        #endregion Command Validator & Handler Error Code Tests
    }
}
