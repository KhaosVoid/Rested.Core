namespace Rested.Core.Data.Search
{
    public interface ISearchResults<TResult>
    {
        int TotalPages { get; set; }
        long TotalQueriedRecords { get; set; }
        long TotalRecords { get; set; }
        List<TResult> Items { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
        List<FieldSortInfo> SortingFields { get; set; }
        List<FieldFilterInfo> FieldFilters { get; set; }
    }
}
