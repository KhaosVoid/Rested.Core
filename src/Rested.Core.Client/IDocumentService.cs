using Rested.Core.Data;

namespace Rested.Core.Client
{
    public interface IDocumentService<TData> where TData : IData
    {
        Task<IDocument<TData>> GetDocument(Guid id);
        Task<List<IDocument<TData>>> GetDocuments();
        Task<SearchDocumentsResults<TData, IDocument<TData>>> SearchDocuments(SearchRequest searchRequest);
        Task<IDocument<TData>> InsertDocument(TData data);
        Task<List<IDocument<TData>>> InsertMultipleDocuments(List<TData> data);
        Task<IDocument<TData>> UpdateDocument(Guid id, byte[] etag, TData data);
        Task<List<IDocument<TData>>> UpdateMultipleDocuments(List<Dto<TData>> dtos);
        Task<IDocument<TData>> PatchDocument(Guid id, byte[] etag, TData data);
        Task<List<IDocument<TData>>> PatchMultipleDocuments(List<Dto<TData>> dtos);
        Task DeleteDocument(Guid id, byte[] etag);
        Task DeleteMultipleDocuments(List<BaseDto> baseDtos);
    }
}
