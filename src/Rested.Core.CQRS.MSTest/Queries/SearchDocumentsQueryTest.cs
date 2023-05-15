using FluentAssertions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.CQRS.Data;
using Rested.Core.CQRS.MSTest;
using Rested.Core.CQRS.Queries;
using Rested.Core.CQRS.Validation;

namespace Rested.Core.MSTest.Queries
{
    public abstract class SearchDocumentsQueryTest<TData, TDocument, TSearchDocumentsQuery, TSearchDocumentsQueryValidator, TSearchDocumentsQueryHandler>
        where TData : IData
        where TDocument : IDocument<TData>
        where TSearchDocumentsQuery : SearchDocumentsQuery<TData, TDocument>
        where TSearchDocumentsQueryValidator : SearchDocumentsQueryValidator<TData, TDocument, TSearchDocumentsQuery>
        where TSearchDocumentsQueryHandler : SearchDocumentsQueryHandler<TData, TDocument, TSearchDocumentsQuery>
    {
        #region Constants

        protected const string TESTCATEGORY_QUERY_VALIDATOR_HANDLER_ERRORCODE_TESTS = "Query Validator & Handler Error Code Tests";
        protected const string TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS = "Query Validation Rule Tests";
        protected const string TESTCATEGORY_QUERY_TESTS = "Query Tests";

        #endregion Constants

        #region Properties

        public TestContext TestContext { get; set; }
        protected List<TDocument> TestDocuments { get; set; }

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
        }

        protected virtual void OnInitializeMockDependencies()
        {
            _loggerFactoryMock = Substitute.For<ILoggerFactory>();
        }

        protected abstract void OnInitializeTestDocuments();
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

        protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
        protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

        protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
        protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

        protected abstract TSearchDocumentsQuery CreateSearchDocumentsQuery(SearchRequest searchRequest);
        protected abstract TSearchDocumentsQueryValidator CreateSearchDocumentsQueryValidator();
        protected abstract TSearchDocumentsQueryHandler CreateSearchDocumentsQueryHandler();

        protected ValidationResult ExecuteQueryValidation(SearchRequest searchRequest) =>
            CreateSearchDocumentsQueryValidator().Validate(CreateSearchDocumentsQuery(searchRequest));

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
            CreateSearchDocumentsQueryValidator().ServiceErrorCodes.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATOR_HANDLER_ERRORCODE_TESTS)]
        public void IsQueryHandlerErrorCodesAssigned()
        {
            CreateSearchDocumentsQueryHandler().ServiceErrorCodes.Should().NotBeNull();
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
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.PageMustBeGreaterThanZero);
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
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.PageInvalid);
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
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.SortingFieldNameIsRequired);
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
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.SortingFieldNameIsInvalid);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void FieldFilterNameIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FilterValue = "testValue"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterNameIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void FieldFilterNameIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = "invalidField",
                            FilterValue = "testValue"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterNameIsInvalid);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void TextFieldFilterValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Text,
                            FilterOperation = (FieldFilterOperations)TextFieldFilterOperations.Contains
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void TextFieldFilterOperationNotSupportedValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Text,
                            FilterOperation = FieldFilterOperations.And,
                            FilterValue = "testValue"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void NumberFieldFilterValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Number,
                            FilterOperation = (FieldFilterOperations)NumberFieldFilterOperations.Equals
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void NumberFieldFilterValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Number,
                            FilterOperation = (FieldFilterOperations)NumberFieldFilterOperations.Equals,
                            FilterValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.Number });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void NumberFieldFilterOperationNotSupportedValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Number,
                            FilterOperation = FieldFilterOperations.And,
                            FilterValue = "123"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void NumberFieldFilterToValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Number,
                            FilterOperation = (FieldFilterOperations)NumberFieldFilterOperations.InRange,
                            FilterValue = "0"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void NumberFieldFilterToValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Number,
                            FilterOperation = (FieldFilterOperations)NumberFieldFilterOperations.InRange,
                            FilterValue = "0",
                            FilterToValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.Number });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateFieldFilterValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Date,
                            FilterOperation = (FieldFilterOperations)DateOnlyFieldFilterOperations.Equals
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateFieldFilterValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Date,
                            FilterOperation = (FieldFilterOperations)DateOnlyFieldFilterOperations.Equals,
                            FilterValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.Date });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateFieldFilterOperationNotSupportedValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Date,
                            FilterOperation = FieldFilterOperations.And,
                            FilterValue = DateOnly.FromDateTime(DateTime.Now).ToString()
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateFieldFilterToValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Date,
                            FilterOperation = (FieldFilterOperations)DateOnlyFieldFilterOperations.InRange,
                            FilterValue = DateOnly.FromDateTime(DateTime.Now).ToString()
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateFieldFilterToValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Date,
                            FilterOperation = (FieldFilterOperations)DateOnlyFieldFilterOperations.InRange,
                            FilterValue = DateOnly.FromDateTime(DateTime.Now).ToString(),
                            FilterToValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.Date });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateTimeFieldFilterValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.DateTime,
                            FilterOperation = (FieldFilterOperations)DateTimeFieldFilterOperations.Equals
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateTimeFieldFilterValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.DateTime,
                            FilterOperation = (FieldFilterOperations)DateTimeFieldFilterOperations.Equals,
                            FilterValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.DateTime });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateTimeFieldFilterOperationNotSupportedValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.DateTime,
                            FilterOperation = FieldFilterOperations.And,
                            FilterValue = DateTime.Now.ToString()
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateTimeFieldFilterToValueIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.DateTime,
                            FilterOperation = (FieldFilterOperations)DateTimeFieldFilterOperations.InRange,
                            FilterValue = DateTime.Now.ToString()
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void DateTimeFieldFilterToValueTypeIsInvalidValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.DateTime,
                            FilterOperation = (FieldFilterOperations)DateTimeFieldFilterOperations.InRange,
                            FilterValue = DateTime.Now.ToString(),
                            FilterToValue = "test"
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid,
                messageFormatArgs: new object[] { FieldFilterTypes.DateTime });
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void CombinedFieldFilterOperationNotSupportedValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Combined,
                            FilterOperation = FieldFilterOperations.Equals,
                            FilterCondition1 = new FieldFilterInfo()
                            {
                                FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                                FilterType = FieldFilterTypes.Text,
                                FilterOperation = (FieldFilterOperations)TextFieldFilterOperations.Contains,
                                FilterValue = "testValue"
                            },
                            FilterCondition2 = new FieldFilterInfo()
                            {
                                FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                                FilterType = FieldFilterTypes.Text,
                                FilterOperation = (FieldFilterOperations)TextFieldFilterOperations.Contains,
                                FilterValue = "testValue2"
                            }
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void CombinedFieldFilterFirstConditionIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Combined,
                            FilterOperation = (FieldFilterOperations)CombinedFieldFilterOperations.Or,
                            FilterCondition2 = new FieldFilterInfo()
                            {
                                FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                                FilterType = FieldFilterTypes.Text,
                                FilterOperation = (FieldFilterOperations)TextFieldFilterOperations.Contains,
                                FilterValue = "testValue2"
                            }
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterFirstConditionIsRequired);
        }

        [TestMethod]
        [TestCategory(TESTCATEGORY_QUERY_VALIDATION_RULE_TESTS)]
        public void CombinedFieldFilterSecondConditionIsRequiredValidation()
        {
            TestQueryValidationRule(
                searchRequest: new SearchRequest()
                {
                    Page = 1,
                    FieldFilters = new List<FieldFilterInfo>()
                    {
                        new FieldFilterInfo()
                        {
                            FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                            FilterType = FieldFilterTypes.Combined,
                            FilterOperation = (FieldFilterOperations)CombinedFieldFilterOperations.Or,
                            FilterCondition1 = new FieldFilterInfo()
                            {
                                FieldName = nameof(IIdentifiable.Id).ToCamelCase(),
                                FilterType = FieldFilterTypes.Text,
                                FilterOperation = (FieldFilterOperations)TextFieldFilterOperations.Contains,
                                FilterValue = "testValue"
                            }
                        }
                    }
                },
                serviceErrorCode: CreateSearchDocumentsQueryValidator().ServiceErrorCodes.CommonErrorCodes.FieldFilterSecondConditionIsRequired);
        }

        #endregion Query Validation Rule Tests
    }
}
