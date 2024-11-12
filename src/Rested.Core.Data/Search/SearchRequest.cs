namespace Rested.Core.Data.Search
{
    public record SearchRequest
    {
        #region Properties

        public virtual int PageSize { get; set; }
        public int Page { get; set; } = 1;
        public List<FieldSortInfo> SortingFields { get; set; }
        public List<FieldFilterInfo> FieldFilters { get; set; }

        #endregion Properties
    }
}
