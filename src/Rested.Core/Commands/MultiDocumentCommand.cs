using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.Commands
{
    public abstract class MultiDocumentCommand<TData, TDocument> : IMultiCommand<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Properties

        public CommandActions Action { get; }

        #endregion Properties

        #region Ctor

        public MultiDocumentCommand(CommandActions action)
        {
            Action = action;
        }

        #endregion Ctor
    }

    public abstract class MultiDocumentCommandValidator<TData, TDocument, TMultiDocumentCommand> : AbstractValidator<TMultiDocumentCommand>, IMultiCommandValidator
        where TData : IData
        where TDocument : IDocument<TData>
        where TMultiDocumentCommand : MultiDocumentCommand<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Ctor

        public MultiDocumentCommandValidator()
        {

        }

        #endregion Ctor
    }

    public abstract class MultiDocumentCommandHandler<TData, TDocument, TMultiDocumentCommand> : IMultiCommandHandler<TDocument, TMultiDocumentCommand>
        where TData : IData
        where TDocument : IDocument<TData>
        where TMultiDocumentCommand : MultiDocumentCommand<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Members

        protected readonly ILogger _logger;

        #endregion Members

        #region Ctor

        public MultiDocumentCommandHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger(GetType());
        }

        #endregion Ctor

        #region Methods

        public void CheckDependencies()
        {
            OnCheckDependencies();

            if (_logger is null)
                throw new NullReferenceException(
                    message: $"{nameof(ILoggerFactory)} was not injected.");
        }

        protected virtual void OnCheckDependencies() { }

        protected abstract List<TDocument> CreateDocumentListFromCommand(TMultiDocumentCommand command);

        public abstract Task<List<TDocument>> Handle(TMultiDocumentCommand command, CancellationToken cancellationToken);

        protected virtual void OnBeginHandle(TMultiDocumentCommand command, IEnumerable<TDocument> documents) { }

        protected abstract Task<List<TDocument>> GetDocuments(Guid[] ids);

        protected virtual void OnHandleComplete(TMultiDocumentCommand command, IEnumerable<TDocument> committedDocuments) { }

        #endregion Methods
    }
}
