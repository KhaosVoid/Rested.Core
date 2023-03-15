using System.Linq.Expressions;

namespace Rested.Core.Data
{
    public interface IDocument<TData> : IPersistedDocument where TData : IData
    {
        byte[] ETag { get; set; }
        TData Data { get; set; }
    }
}
