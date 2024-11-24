using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class DateFieldFilter : FieldFilter<DateOnly?, DateFieldFilterOperations>
{
    #region Properties
    
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.DateFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override DateFieldFilterOperations FilterOperation { get; set; }
    
    #endregion Properties
    
    #region Ctor

    public DateFieldFilter()
    {
        
    }

    public DateFieldFilter(DateFieldFilter dateFieldFilter) : base(dateFieldFilter)
    {
        
    }
    
    #endregion Ctor
}