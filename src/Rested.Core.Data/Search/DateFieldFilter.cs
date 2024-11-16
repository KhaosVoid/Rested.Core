using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class DateFieldFilter : FieldFilter<DateOnly?, DateFieldFilterOperations>
{
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.DateFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override DateFieldFilterOperations FilterOperation { get; set; }
}