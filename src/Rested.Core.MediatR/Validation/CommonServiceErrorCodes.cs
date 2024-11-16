using System.Net;

namespace Rested.Core.MediatR.Validation;

public class CommonServiceErrorCodes : ServiceErrorCodesDictionary
{
    #region Properties

    public ServiceErrorCode IDIsRequired => this[nameof(IDIsRequired)];
    public ServiceErrorCode ETagIsRequired => this[nameof(ETagIsRequired)];
    public ServiceErrorCode DataIsRequired => this[nameof(DataIsRequired)];
    public ServiceErrorCode CollectionMustNotBeEmpty => this[nameof(CollectionMustNotBeEmpty)];
    public ServiceErrorCode QueryError => this[nameof(QueryError)];
    public ServiceErrorCode PageMustBeGreaterThanZero => this[nameof(PageMustBeGreaterThanZero)];
    public ServiceErrorCode PageInvalid => this[nameof(PageInvalid)];
    public ServiceErrorCode PageSizeMustBeGreaterThanOrEqualToMinPageSize => this[nameof(PageSizeMustBeGreaterThanOrEqualToMinPageSize)];
    public ServiceErrorCode PageSizeMustBeLessThanOrEqualToMaxPageSize => this[nameof(PageSizeMustBeLessThanOrEqualToMaxPageSize)];
    public ServiceErrorCode SortingFieldNameIsRequired => this[nameof(SortingFieldNameIsRequired)];
    public ServiceErrorCode SortingFieldNameIsInvalid => this[nameof(SortingFieldNameIsInvalid)];
    public ServiceErrorCode FieldFilterNameIsRequired => this[nameof(FieldFilterNameIsRequired)];
    public ServiceErrorCode FieldFilterNameIsInvalid => this[nameof(FieldFilterNameIsInvalid)];
    public ServiceErrorCode FieldFilterValueIsRequired => this[nameof(FieldFilterValueIsRequired)];
    public ServiceErrorCode FieldFilterToValueIsRequired => this[nameof(FieldFilterToValueIsRequired)];
    public ServiceErrorCode FieldFilterOperationNotSupported => this[nameof(FieldFilterOperationNotSupported)];
    public ServiceErrorCode OperatorFilterFiltersIsRequired => this[nameof(OperatorFilterFiltersIsRequired)];
    public ServiceErrorCode DatabaseError => this[nameof(DatabaseError)];
    public ServiceErrorCode DatabaseIndexViolationError => this[nameof(DatabaseIndexViolationError)];

    #endregion Properties

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
    protected readonly string FIELD_FILTER_TO_VALUE_IS_REQUIRED_MESSAGE = "The filter to value is required.";
    protected readonly string FIELD_FILTER_OPERATION_NOT_SUPPORTED_MESSAGE = "The specified filter operation is not supported.";
    protected readonly string OPERATOR_FILTER_FILTERS_IS_REQUIRED_MESSAGE = "The Filters property is required.";
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
        Add(nameof(IDIsRequired), ID_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(ETagIsRequired), ETAG_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(DataIsRequired), DATA_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(CollectionMustNotBeEmpty), COLLECTION_MUST_NOT_BE_EMPTY_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(QueryError), QUERY_ERROR_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(PageMustBeGreaterThanZero), PAGE_MUST_BE_GREATER_THAN_ZERO_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(PageInvalid), PAGE_INVALID_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(PageSizeMustBeGreaterThanOrEqualToMinPageSize), PAGE_SIZE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_MIN_PAGE_SIZE_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(PageSizeMustBeLessThanOrEqualToMaxPageSize), PAGE_SIZE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MAX_PAGE_SIZE_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(SortingFieldNameIsRequired), SORTING_FIELD_NAME_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(SortingFieldNameIsInvalid), SORTING_FIELD_NAME_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(FieldFilterNameIsRequired), FIELD_FILTER_NAME_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(FieldFilterNameIsInvalid), FIELD_FILTER_NAME_IS_INVALID_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(FieldFilterValueIsRequired), FIELD_FILTER_VALUE_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(FieldFilterToValueIsRequired), FIELD_FILTER_TO_VALUE_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(FieldFilterOperationNotSupported), FIELD_FILTER_OPERATION_NOT_SUPPORTED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(OperatorFilterFiltersIsRequired), OPERATOR_FILTER_FILTERS_IS_REQUIRED_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(DatabaseError), DATABASE_ERROR_MESSAGE, HttpStatusCode.BadRequest);
        Add(nameof(DatabaseIndexViolationError), DATABASE_INDEX_VIOLATION_ERROR_MESSAGE, HttpStatusCode.BadRequest);
    }

    #endregion Methods
}