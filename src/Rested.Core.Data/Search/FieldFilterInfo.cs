namespace Rested.Core.Data.Search
{
    public class FieldFilterInfo
    {
        public string FieldName { get; set; }
        public FieldFilterTypes FilterType { get; set; }
        public FieldFilterOperations FilterOperation { get; set; }
        public string FilterValue { get; set; }
        public string FilterToValue { get; set; }
        public FieldFilterInfo FilterCondition1 { get; set; }
        public FieldFilterInfo FilterCondition2 { get; set; }
    }
}
