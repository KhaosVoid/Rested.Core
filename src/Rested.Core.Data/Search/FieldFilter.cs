using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public abstract class FieldFilter<TValue, TFilterOperations> : IFieldFilter<TValue, TFilterOperations> where TFilterOperations : Enum
{
    [JsonPropertyOrder(0)]
    public abstract FilterTypes FilterType { get; set; }
    
    [JsonPropertyOrder(1)]
    public string FieldName { get; set; }
    
    [JsonPropertyOrder(2)]
    public abstract TFilterOperations FilterOperation { get; set; }
    
    [JsonPropertyOrder(3)]
    public TValue Value { get; set; }
    
    [JsonPropertyOrder(4)]
    public TValue ToValue { get; set; }
}