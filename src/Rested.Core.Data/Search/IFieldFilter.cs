namespace Rested.Core.Data.Search;

public interface IFieldFilter : IFilter
{
    public string FieldName { get; set; }
}

public interface IFieldFilter<TValue, TFilterOperations> : IFieldFilter where TFilterOperations : Enum
{
    public TFilterOperations FilterOperation { get; set; }
    public TValue Value { get; set; }
    public TValue ToValue { get; set; }
}