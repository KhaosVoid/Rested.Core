namespace Rested.Core.Data.Search;

public interface IOperatorFilter : IFilter
{
    public FilterOperators Operator { get; set; }
    public List<IFilter> Filters { get; set; }
}