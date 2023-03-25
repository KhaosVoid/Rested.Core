namespace Rested.Core.CQRS.Data
{
    public interface IDocument<TData> : IPersistedDocument where TData : IData
    {
        byte[] ETag { get; set; }
        TData? Data { get; set; }
    }
}
