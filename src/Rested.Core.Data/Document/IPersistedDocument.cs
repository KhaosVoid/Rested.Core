namespace Rested.Core.Data.Document
{
    public interface IPersistedDocument : IIdentifiable
    {
        string? CreateUser { get; set; }
        DateTime? CreateDateTime { get; set; }
        string? UpdateUser { get; set; }
        DateTime? UpdateDateTime { get; set; }
        ulong UpdateVersion { get; set; }
    }
}
