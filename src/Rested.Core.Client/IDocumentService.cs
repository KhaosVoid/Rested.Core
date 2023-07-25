using Rested.Core.Data;

namespace Rested.Core.Client
{
    public interface IDocumentService<TData, TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        Task<TDocument> GetDocument(Guid id);
        Task<List<TDocument>> GetDocuments();
        Task<SearchDocumentsResults<TData, TDocument>> SearchDocuments(SearchRequest searchRequest);
        Task<TDocument> InsertDocument(TData data);
        Task<List<TDocument>> InsertMultipleDocuments(List<TData> data);
        Task<TDocument> UpdateDocument(Guid id, byte[] etag, TData data);
        Task<List<TDocument>> UpdateMultipleDocuments(List<Dto<TData>> dtos);
        Task<TDocument> PatchDocument(Guid id, byte[] etag, TData data);
        Task<List<TDocument>> PatchMultipleDocuments(List<Dto<TData>> dtos);
        Task DeleteDocument(Guid id, byte[] etag);
        Task DeleteMultipleDocuments(List<BaseDto> baseDtos);
    }
}
