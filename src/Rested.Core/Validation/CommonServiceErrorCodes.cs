using System.Net;

namespace Rested.Core.Validation
{
    public class CommonServiceErrorCodes : ServiceErrorCodesDictionary
    {
        #region Properties

        public ServiceErrorCode IDIsRequired => this[ID_IS_REQUIRED_NAME];
        public ServiceErrorCode ETagIsRequired => this[ETAG_IS_REQUIRED_NAME];
        public ServiceErrorCode DataIsRequired => this[DATA_IS_REQUIRED_NAME];
        public ServiceErrorCode CollectionMustNotBeEmpty => this[COLLECTION_MUST_NOT_BE_EMPTY_NAME];
        public ServiceErrorCode QueryError => this[QUERY_ERROR_NAME];
        public ServiceErrorCode PageMustBeGreaterThanZero => this[PAGE_MUST_BE_GREATER_THAN_ZERO_NAME];
        public ServiceErrorCode PageInvalid => this[PAGE_INVALID_NAME];
        public ServiceErrorCode PageSizeMustBeGreaterThanOrEqualToMinPageSize => this[PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_NAME];
        public ServiceErrorCode PageSizeMustBeLessThanOrEqualToMaxPageSize => this[PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_NAME];
        public ServiceErrorCode SortingFieldNameIsRequired => this[SORTING_FIELD_NAME_IS_REQUIRED_NAME];
        public ServiceErrorCode SortingFieldNameIsInvalid => this[SORTING_FIELD_NAME_IS_INVALID_NAME];
        public ServiceErrorCode FieldFilterNameIsRequired => this[FIELD_FILTER_NAME_IS_REQUIRED_NAME];
        public ServiceErrorCode FieldFilterNameIsInvalid => this[FIELD_FILTER_NAME_IS_INVALID_NAME];
        public ServiceErrorCode FieldFilterValueIsRequired => this[FIELD_FILTER_VALUE_IS_REQUIRED_NAME];
        public ServiceErrorCode FieldFilterValueTypeIsInvalid => this[FIELD_FILTER_VALUE_TYPE_IS_INVALID_NAME];
        public ServiceErrorCode FieldFilterToValueIsRequired => this[FIELD_FILTER_TO_VALUE_IS_REQUIRED_NAME];
        public ServiceErrorCode FieldFilterToValueTypeIsInvalid => this[FIELD_FILTER_TO_VALUE_TYPE_IS_INVALID_NAME];
        public ServiceErrorCode FieldFilterFirstConditionIsRequired => this[FIELD_FILTER_FIRST_CONDITION_IS_REQUIRED_NAME];
        public ServiceErrorCode FieldFilterSecondConditionIsRequired => this[FIELD_FILTER_SECOND_CONDITION_IS_REQUIRED_NAME];
        public ServiceErrorCode FieldFilterOperationNotSupported => this[FIELD_FILTER_OPERATION_NOT_SUPPORTED_NAME];
        public ServiceErrorCode DatabaseError => this[DATABASE_ERROR_NAME];
        public ServiceErrorCode DatabaseIndexViolationError => this[DATABASE_INDEX_VIOLATION_ERROR_NAME];

        #endregion Properties

        #region Error Code Names

        protected readonly string ID_IS_REQUIRED_NAME = "IDIsRequired";
        protected readonly string ETAG_IS_REQUIRED_NAME = "ETagIsRequired";
        protected readonly string DATA_IS_REQUIRED_NAME = "DataIsRequired";
        protected readonly string COLLECTION_MUST_NOT_BE_EMPTY_NAME = "CollectionMustNotBeEmpty";
        protected readonly string QUERY_ERROR_NAME = "QueryError";
        protected readonly string PAGE_MUST_BE_GREATER_THAN_ZERO_NAME = "PageMustBeGreaterThanZero";
        protected readonly string PAGE_INVALID_NAME = "PageInvalid";
        protected readonly string PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_NAME = "PageSizeMustBeGreaterThanOrEqualToMinPageSize";
        protected readonly string PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_NAME = "PageSizeMustBeLessThanOrEqualToMaxPageSize";
        protected readonly string SORTING_FIELD_NAME_IS_REQUIRED_NAME = "SortingFieldNameIsRequired";
        protected readonly string SORTING_FIELD_NAME_IS_INVALID_NAME = "SortingFieldNameIsInvalid";
        protected readonly string FIELD_FILTER_NAME_IS_REQUIRED_NAME = "FieldFilterNameIsRequired";
        protected readonly string FIELD_FILTER_NAME_IS_INVALID_NAME = "FieldFilterNameIsInvalid";
        protected readonly string FIELD_FILTER_VALUE_IS_REQUIRED_NAME = "FieldFilterValueIsRequired";
        protected readonly string FIELD_FILTER_VALUE_TYPE_IS_INVALID_NAME = "FieldFilterValueTypeIsInvalid";
        protected readonly string FIELD_FILTER_TO_VALUE_IS_REQUIRED_NAME = "FieldFilterToValueIsRequired";
        protected readonly string FIELD_FILTER_TO_VALUE_TYPE_IS_INVALID_NAME = "FieldFilterToValueTypeIsInvalid";
        protected readonly string FIELD_FILTER_FIRST_CONDITION_IS_REQUIRED_NAME = "FieldFilterFirstConditionIsRequired";
        protected readonly string FIELD_FILTER_SECOND_CONDITION_IS_REQUIRED_NAME = "FieldFilterSecondConditionIsRequired";
        protected readonly string FIELD_FILTER_OPERATION_NOT_SUPPORTED_NAME = "FieldFilterOperationNotSupported";
        protected readonly string DATABASE_ERROR_NAME = "DatabaseError";
        protected readonly string DATABASE_INDEX_VIOLATION_ERROR_NAME = "DatabaseIndexViolationError";

        #endregion Error Code Names

        #region Error Code Messages

        protected readonly string ID_IS_REQUIRED_MESSAGE = "ID is required.";
        protected readonly string ETAG_IS_REQUIRED_MESSAGE = "ETag is required.";
        protected readonly string DATA_IS_REQUIRED_MESSAGE = "Data is required.";
        protected readonly string COLLECTION_MUST_NOT_BE_EMPTY_MESSAGE = "Collection must not be empty.";
        protected readonly string QUERY_ERROR_MESSAGE = "An error occurred while querying the data.";
        protected readonly string PAGE_MUST_BE_GREATER_THAN_ZERO_MESSAGE = "The specified page must be greater than zero.";
        protected readonly string PAGE_INVALID_MESSAGE = "The specified page is invalid.";
        protected readonly string PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_MESSAGE = "The specified page size must be greater than or equal to {0}.";
        protected readonly string PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_MESSAGE = "The specified page size must be less than or equal to {0}.";
        protected readonly string SORTING_FIELD_NAME_IS_REQUIRED_MESSAGE = "The sorting field name is required.";
        protected readonly string SORTING_FIELD_NAME_IS_INVALID_MESSAGE = "The specified sorting field name is invalid.";
        protected readonly string FIELD_FILTER_NAME_IS_REQUIRED_MESSAGE = "The filter name is required.";
        protected readonly string FIELD_FILTER_NAME_IS_INVALID_MESSAGE = "The specified filter name is invalid.";
        protected readonly string FIELD_FILTER_VALUE_IS_REQUIRED_MESSAGE = "The filter value is required.";
        protected readonly string FIELD_FILTER_VALUE_TYPE_IS_INVALID_MESSAGE = "The filter value is not a valid {0}.";
        protected readonly string FIELD_FILTER_TO_VALUE_IS_REQUIRED_MESSAGE = "The filter to value is required.";
        protected readonly string FIELD_FILTER_TO_VALUE_TYPE_IS_INVALID_MESSAGE = "The filter to value is not a valid {0}.";
        protected readonly string FIELD_FILTER_FIRST_CONDITION_IS_REQUIRED_MESSAGE = "The first filter condition is required.";
        protected readonly string FIELD_FILTER_SECOND_CONDITION_IS_REQUIRED_MESSAGE = "The second filter condition is required.";
        protected readonly string FIELD_FILTER_OPERATION_NOT_SUPPORTED_MESSAGE = "The specified filter operation is not supported.";
        protected readonly string DATABASE_ERROR_MESSAGE = "Database Error: {0}";
        protected readonly string DATABASE_INDEX_VIOLATION_ERROR_MESSAGE = "A duplicate key error has occurred. IndexName: {0}, IndexValue: {1}";

        #endregion Error Code Messages

        #region Ctor

        public CommonServiceErrorCodes(int serviceId, int featureId) : base(serviceId, featureId)
        {
            OnInitializeCommonErrorCodes();
        }

        #endregion Ctor

        #region Methods

        protected virtual void OnInitializeCommonErrorCodes()
        {
            //  Name                                                           Message                                                           HttpStatusCode             FailureCode  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            Add(ID_IS_REQUIRED_NAME, ID_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 1);
            Add(ETAG_IS_REQUIRED_NAME, ETAG_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 2);
            Add(DATA_IS_REQUIRED_NAME, DATA_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 3);
            Add(COLLECTION_MUST_NOT_BE_EMPTY_NAME, COLLECTION_MUST_NOT_BE_EMPTY_MESSAGE, HttpStatusCode.BadRequest, 4);
            Add(QUERY_ERROR_NAME, QUERY_ERROR_MESSAGE, HttpStatusCode.BadRequest, 5);
            Add(PAGE_MUST_BE_GREATER_THAN_ZERO_NAME, PAGE_MUST_BE_GREATER_THAN_ZERO_MESSAGE, HttpStatusCode.BadRequest, 6);
            Add(PAGE_INVALID_NAME, PAGE_INVALID_MESSAGE, HttpStatusCode.BadRequest, 7);
            Add(PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_NAME, PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_MESSAGE, HttpStatusCode.BadRequest, 8);
            Add(PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_NAME, PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_MESSAGE, HttpStatusCode.BadRequest, 9);
            Add(SORTING_FIELD_NAME_IS_REQUIRED_NAME, SORTING_FIELD_NAME_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 10);
            Add(SORTING_FIELD_NAME_IS_INVALID_NAME, SORTING_FIELD_NAME_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest, 11);
            Add(FIELD_FILTER_NAME_IS_REQUIRED_NAME, FIELD_FILTER_NAME_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 12);
            Add(FIELD_FILTER_NAME_IS_INVALID_NAME, FIELD_FILTER_NAME_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest, 13);
            Add(FIELD_FILTER_VALUE_IS_REQUIRED_NAME, FIELD_FILTER_VALUE_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 14);
            Add(FIELD_FILTER_VALUE_TYPE_IS_INVALID_NAME, FIELD_FILTER_VALUE_TYPE_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest, 15);
            Add(FIELD_FILTER_TO_VALUE_IS_REQUIRED_NAME, FIELD_FILTER_TO_VALUE_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 16);
            Add(FIELD_FILTER_TO_VALUE_TYPE_IS_INVALID_NAME, FIELD_FILTER_TO_VALUE_TYPE_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest, 17);
            Add(FIELD_FILTER_FIRST_CONDITION_IS_REQUIRED_NAME, FIELD_FILTER_FIRST_CONDITION_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 18);
            Add(FIELD_FILTER_SECOND_CONDITION_IS_REQUIRED_NAME, FIELD_FILTER_SECOND_CONDITION_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest, 19);
            Add(FIELD_FILTER_OPERATION_NOT_SUPPORTED_NAME, FIELD_FILTER_OPERATION_NOT_SUPPORTED_MESSAGE, HttpStatusCode.BadRequest, 20);
            Add(DATABASE_ERROR_NAME, DATABASE_ERROR_MESSAGE, HttpStatusCode.BadRequest, 21);
            Add(DATABASE_INDEX_VIOLATION_ERROR_NAME, DATABASE_INDEX_VIOLATION_ERROR_MESSAGE, HttpStatusCode.BadRequest, 22);
        }

        #endregion Methods
    }
}
