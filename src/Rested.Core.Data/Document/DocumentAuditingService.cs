namespace Rested.Core.Data.Document;

public class DocumentAuditingService : IDocumentAuditingService
{
    public virtual void SetDocumentAuditingInformation<TData, TDocument>(TDocument document, bool isUpdate = false)
        where TData : IData
        where TDocument : IDocument<TData>
    {
        if (isUpdate)
        {
            document.UpdateDateTime = DateTime.UtcNow;
            document.UpdateVersion++;
        }

        else
        {
            document.CreateDateTime = DateTime.UtcNow;
            document.UpdateDateTime = DateTime.UtcNow;
        }
    }
}