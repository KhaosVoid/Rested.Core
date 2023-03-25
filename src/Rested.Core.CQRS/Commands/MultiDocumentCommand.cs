using Microsoft.Extensions.Logging;
using Rested.Core.CQRS.Data;

namespace Rested.Core.CQRS.Commands
{
    public abstract class MultiDocumentCommand<TData, TDocument> : Command<List<TDocument>>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Ctor

        public MultiDocumentCommand(CommandActions action) : base(action)
        {

        }

        #endregion Ctor
    }

    public abstract class MultiDocumentCommandValidator<TData, TDocument, TMultiDocumentCommand> : CommandValidator<List<TDocument>, TMultiDocumentCommand>
        where TData : IData
        where TDocument : IDocument<TData>
        where TMultiDocumentCommand : MultiDocumentCommand<TData, TDocument>
    {
        #region Ctor

        public MultiDocumentCommandValidator() : base()
        {

        }

        #endregion Ctor
    }

    public abstract class MultiDocumentCommandHandler<TData, TDocument, TMultiDocumentCommand> : CommandHandler<List<TDocument>, TMultiDocumentCommand>
        where TData : IData
        where TDocument : IDocument<TData>
        where TMultiDocumentCommand : MultiDocumentCommand<TData, TDocument>
    {
        #region Ctor

        public MultiDocumentCommandHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
        {

        }

        #endregion Ctor

        #region Methods

        protected abstract List<TDocument> CreateDocumentListFromCommand(TMultiDocumentCommand command);

        protected virtual void OnBeginHandle(TMultiDocumentCommand command, IEnumerable<TDocument> documents) { }

        protected virtual void OnSetPrecalculatedProperties(TMultiDocumentCommand command, IEnumerable<TDocument> documents) { }

        protected abstract Task<List<TDocument>> GetDocuments(Guid[] ids);

        protected virtual void OnHandleComplete(TMultiDocumentCommand command, IEnumerable<TDocument> committedDocuments) { }

        #endregion Methods
    }
}
