using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class TextFieldFilter : FieldFilter<string, TextFieldFilterOperations>
{
    #region Properties
    
    [JsonPropertyOrder(0)]
    public override FilterTypes FilterType { get; set; } = FilterTypes.TextFieldFilter;
    
    [JsonPropertyOrder(2)]
    public override TextFieldFilterOperations FilterOperation { get; set; }
    
    #endregion Properties
    
    #region Ctor

    public TextFieldFilter()
    {
        
    }

    public TextFieldFilter(TextFieldFilter textFieldFilter) : base(textFieldFilter)
    {
        
    }
    
    #endregion Ctor
}