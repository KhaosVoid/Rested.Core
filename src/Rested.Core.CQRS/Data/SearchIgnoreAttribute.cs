namespace Rested.Core.CQRS.Data
{
    /// <summary>
    /// Indicates that this property should be ignored when a search request is being performed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchIgnoreAttribute : Attribute
    {

    }
}
