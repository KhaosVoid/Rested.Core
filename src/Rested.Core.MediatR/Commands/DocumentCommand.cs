using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Data.Document;

namespace Rested.Core.MediatR.Commands;

public abstract class DocumentCommand<TData, TDocument> : Command<TDocument>
    where TData : IData
    where TDocument : IDocument<TData>
{
    #region Ctor

    public DocumentCommand(CommandActions action) : base(action)
    {

    }

    #endregion Ctor
}

public abstract class DocumentCommandValidator<TData, TDocument, TDocumentCommand> : CommandValidator<TDocument, TDocumentCommand>
    where TData : IData
    where TDocument : IDocument<TData>
    where TDocumentCommand : DocumentCommand<TData, TDocument>
{
    #region Ctor

    public DocumentCommandValidator() : base()
    {

    }

    #endregion Ctor
}

public abstract class DocumentCommandHandler<TData, TDocument, TDocumentCommand> : CommandHandler<TDocument, TDocumentCommand>
    where TData : IData
    where TDocument : IDocument<TData>
    where TDocumentCommand : DocumentCommand<TData, TDocument>
{
    #region Ctor

    public DocumentCommandHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
    {

    }

    #endregion Ctor

    #region Methods

    protected abstract TDocument CreateDocumentFromCommand(TDocumentCommand command);

    protected virtual void OnBeginHandle(TDocumentCommand command, TDocument document) { }

    protected virtual void OnSetPrecalculatedProperties(TDocumentCommand command, TDocument document) { }

    protected abstract Task<TDocument> GetDocument(Guid id);

    protected virtual void OnHandleComplete(TDocumentCommand command, TDocument committedDocument) { }

    #endregion Methods
}