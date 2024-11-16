using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class NumberFieldFilter : FieldFilter<int?, NumberFieldFilterOperations>
{
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.NumberFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override NumberFieldFilterOperations FilterOperation { get; set; }
}