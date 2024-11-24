using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class NumberFieldFilter : FieldFilter<int?, NumberFieldFilterOperations>
{
    #region Properties
    
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.NumberFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override NumberFieldFilterOperations FilterOperation { get; set; }
    
    #endregion Properties
    
    #region Ctor

    public NumberFieldFilter()
    {
        
    }

    public NumberFieldFilter(NumberFieldFilter numberFieldFilter) : base(numberFieldFilter)
    {
        
    }
    
    #endregion Ctor
}