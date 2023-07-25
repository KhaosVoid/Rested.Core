namespace Rested.Core.Data
{
    public abstract class SearchResults<TResultType> : SearchRequest, ISearchResults<TResultType>
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
                FieldFilters = searchRequest.FieldFilters;
            }
        }

        #endregion Ctor
    }
}
