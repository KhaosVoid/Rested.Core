namespace Rested.Core.Data
{
    public interface IDocumentAuditingService
    {
        void SetDocumentAuditingInformation<TData, TDocument>(TDocument document, bool isUpdate = false)
            where TData : IData
            where TDocument : IDocument<TData>;
    }
}
