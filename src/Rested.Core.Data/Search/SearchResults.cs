namespace Rested.Core.Data.Search;

public abstract record SearchResults<TResultType> : SearchRequest, ISearchResults<TResultType>
{
    #region Properties

    public int TotalPages { get; set; }
    public long TotalQueriedRecords { get; set; }
    public long TotalRecords { get; set; }
    public List<TResultType> Items { get; set; }

    #endregion Properties

    #region Ctor

    public SearchResults(SearchRequest searchRequest)
    {
        if (searchRequest is not null)
        {
            PageSize = searchRequest.PageSize;
            Page = searchRequest.Page;
            SortingFields = searchRequest.SortingFields;
            Filters = searchRequest.Filters;
        }
    }

    #endregion Ctor
}