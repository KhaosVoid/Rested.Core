namespace Rested.Core.Data.Document;

public interface IDocument<TData> : IPersistedDocument where TData : IData
{
    byte[] ETag { get; set; }
    TData? Data { get; set; }
}