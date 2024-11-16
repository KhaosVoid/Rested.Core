using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class DateTimeFieldFilter : FieldFilter<DateTime?, DateTimeFieldFilterOperations>
{
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.DateTimeFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override DateTimeFieldFilterOperations FilterOperation { get; set; }
}