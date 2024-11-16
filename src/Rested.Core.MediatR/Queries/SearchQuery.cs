using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries;

public abstract class SearchQuery<TResultType, TSearchResults> : IQuery<TSearchResults>
    where TSearchResults : SearchResults<TResultType>
{
    #region Properties

    public SearchRequest SearchRequest { get; set; }
    public int MinPageSize { get; protected set; }
    public int MaxPageSize { get; protected set; }
    public int DefaultPageSize { get; protected set; }

    #endregion Properties

    #region Ctor

    public SearchQuery(SearchRequest searchRequest, int minPageSize = 1, int maxPageSize = 100, int defaultPageSize = 25)
    {
        SearchRequest = searchRequest;

        SetPageSizeBoundaries(minPageSize, maxPageSize, defaultPageSize);

        if (SearchRequest.PageSize <= -1)
            SearchRequest.PageSize = MaxPageSize;

        else if (SearchRequest.PageSize is 0)
            SearchRequest.PageSize = DefaultPageSize;
    }

    #endregion Ctor

    #region Methods

    protected void SetPageSizeBoundaries(int minPageSize, int maxPageSize, int defaultPageSize)
    {
        if (maxPageSize <= 0)
            minPageSize = 1;

        else if (minPageSize > maxPageSize)
            minPageSize = maxPageSize;

        if (maxPageSize < minPageSize)
            maxPageSize = minPageSize;

        if (defaultPageSize < minPageSize)
            defaultPageSize = minPageSize;

        else if (defaultPageSize > maxPageSize)
            defaultPageSize = maxPageSize;

        MinPageSize = minPageSize;
        MaxPageSize = maxPageSize;
        DefaultPageSize = defaultPageSize;
    }

    #endregion Methods
}

public abstract class SearchQueryValidator<TResultType, TSearchResults, TSearchQuery> :
    AbstractValidator<TSearchQuery>, IQueryValidator
    where TSearchResults : SearchResults<TResultType>
    where TSearchQuery : SearchQuery<TResultType, TSearchResults>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Ctor

    public SearchQueryValidator()
    {
        DataUtility.GenerateValidSearchFieldNames<TResultType>(out var validFieldNames, out var ignoredFieldNames);

        RuleFor(query => query.SearchRequest.Page)
            .GreaterThan(0)
            .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.PageMustBeGreaterThanZero);

        When(
            predicate: query => query.SearchRequest.Page > 0,
            action: () =>
            {
                RuleFor(query => query.SearchRequest)
                    .Must(searchRequest => (searchRequest.Page - 1U) * searchRequest.PageSize <= int.MaxValue)
                    .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.PageInvalid);
            });

        RuleFor(query => query.SearchRequest.PageSize)
            .GreaterThanOrEqualTo(query => query.MinPageSize)
            .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.PageSizeMustBeGreaterThanOrEqualToMinPageSize, query => [query.MinPageSize]);

        RuleFor(query => query.SearchRequest.PageSize)
            .LessThanOrEqualTo(query => query.MaxPageSize)
            .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.PageSizeMustBeLessThanOrEqualToMaxPageSize, query => [query.MaxPageSize]);

        When(
            predicate: query => query.SearchRequest.SortingFields is not null,
            action: () => RuleForEach(query => query.SearchRequest.SortingFields).SetValidator(new FieldSortInfoValidator(validFieldNames, ignoredFieldNames, ServiceErrorCodes)));

        RuleFor(query => query.SearchRequest.Filters).SetValidator(new FiltersValidator(validFieldNames, ignoredFieldNames, ServiceErrorCodes));
    }

    #endregion Ctor
}

public abstract class SearchQueryHandler<TResultType, TSearchResults, TSearchQuery> :
    IQueryHandler<TSearchResults, TSearchQuery>
    where TSearchResults : SearchResults<TResultType>
    where TSearchQuery : SearchQuery<TResultType, TSearchResults>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Members

    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public SearchQueryHandler(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
    }

    #endregion Ctor

    #region Methods

    public void CheckDependencies()
    {
        OnCheckDependencies();

        if (_logger is null)
            throw new NullReferenceException(
                message: $"{nameof(ILoggerFactory)} was not injected.");
    }

    protected virtual void OnCheckDependencies() { }

    public abstract Task<TSearchResults> Handle(TSearchQuery query, CancellationToken cancellationToken);

    protected virtual void OnBeginHandle(TSearchQuery query) { }

    protected virtual List<IFilter> GetImplicitFilters() => new();

    protected abstract Task<TSearchResults> GetSearchResults(TSearchQuery query);

    protected virtual void OnHandleComplete(TSearchQuery query, TSearchResults searchResults) { }

    #endregion Methods
}