using System.Text.Json.Serialization;

namespace Rested.Core.Data.Search;

public class OperatorFilter : IOperatorFilter
{
    #region Properties
    
    [JsonPropertyOrder(0)]
    public FilterTypes FilterType { get; set; } = FilterTypes.OperatorFilter;
    
    [JsonPropertyOrder(1)]
    public FilterOperators Operator { get; set; }
    
    [JsonPropertyOrder(2)]
    public List<IFilter> Filters { get; set; }
    
    #endregion Properties
    
    #region Ctor

    public OperatorFilter()
    {
        
    }

    public OperatorFilter(OperatorFilter operatorFilter)
    {
        Operator = operatorFilter.Operator;

        var filters = new List<IFilter>();

        operatorFilter.Filters.ForEach(filter =>
        {
            switch (filter)
            {
                case TextFieldFilter newTextFieldFilter:
                    filters.Add(new TextFieldFilter(newTextFieldFilter));
                    break;
                
                case NumberFieldFilter newNumberFieldFilter:
                    filters.Add(new NumberFieldFilter(newNumberFieldFilter));
                    break;
                
                case DateFieldFilter newDateFieldFilter:
                    filters.Add(new DateFieldFilter(newDateFieldFilter));
                    break;
                
                case DateTimeFieldFilter newDateTimeFieldFilter:
                    filters.Add(new DateTimeFieldFilter(newDateTimeFieldFilter));
                    break;
                
                case OperatorFilter newOperatorFilter:
                    filters.Add(new OperatorFilter(newOperatorFilter));
                    break;
            }
        });
        
        Filters = filters;
    }
    
    #endregion Ctor
}