using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class OperatorFilter : IOperatorFilter
{
    [JsonPropertyOrder(0)]
    public FilterTypes FilterType { get; set; } = FilterTypes.OperatorFilter;
    
    [JsonPropertyOrder(1)]
    public FilterOperators Operator { get; set; }
    
    [JsonPropertyOrder(2)]
    public List<IFilter> Filters { get; set; }
}