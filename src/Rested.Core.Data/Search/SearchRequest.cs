namespace Rested.Core.Data.Search;

public record SearchRequest
{
    public virtual int PageSize { get; set; }
    public int Page { get; set; } = 1;
    public List<FieldSortInfo>? SortingFields { get; set; }
    public List<IFilter>? Filters { get; set; }
}
