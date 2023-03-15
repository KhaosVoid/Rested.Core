using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.Commands
{
    public abstract class DocumentCommand<TData, TDocument> : ICommand<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Properties

        public CommandActions Action { get; }

        #endregion Properties

        #region Ctor

        public DocumentCommand(CommandActions action)
        {
            Action = action;
        }

        #endregion Ctor
    }

    public abstract class DocumentCommandValidator<TData, TDocument, TDocumentCommand> : AbstractValidator<TDocumentCommand>, ICommandValidator
        where TData : IData
        where TDocument : IDocument<TData>
        where TDocumentCommand : DocumentCommand<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Ctor

        public DocumentCommandValidator()
        {

        }

        #endregion Ctor
    }

    public abstract class DocumentCommandHandler<TData, TDocument, TDocumentCommand> : ICommandHandler<TDocument, TDocumentCommand>
        where TData : IData
        where TDocument : IDocument<TData>
        where TDocumentCommand : DocumentCommand<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Members

        protected readonly ILogger _logger;

        #endregion Members

        #region Ctor

        public DocumentCommandHandler(ILoggerFactory loggerFactory)
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

        protected abstract TDocument CreateDocumentFromCommand(TDocumentCommand command);

        public abstract Task<TDocument> Handle(TDocumentCommand command, CancellationToken cancellationToken);

        protected virtual void OnBeginHandle(TDocumentCommand command, TDocument document) { }

        protected abstract Task<TDocument> GetDocument(Guid id);

        protected virtual void OnHandleComplete(TDocumentCommand command, TDocument committedDocument) { }

        #endregion Methods
    }
}
