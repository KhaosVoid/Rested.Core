using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries
{
    public abstract class GetDocumentQuery<TData, TDocument> : IQuery<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        #region Properties

        public Guid Id { get; }

        #endregion Properties

        #region Ctor

        public GetDocumentQuery(Guid id)
        {
            Id = id;
        }

        #endregion Ctor
    }

    public abstract class GetDocumentQueryValidator<TData, TDocument, TGetDocumentQuery> : AbstractValidator<TGetDocumentQuery>, IQueryValidator
        where TData : IData
        where TDocument : IDocument<TData>
        where TGetDocumentQuery : GetDocumentQuery<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Ctor

        public GetDocumentQueryValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.IDIsRequired);
        }

        #endregion Ctor
    }

    public abstract class GetDocumentQueryHandler<TData, TDocument, TGetDocumentQuery> : IQueryHandler<TDocument, TGetDocumentQuery>
        where TData : IData
        where TDocument : IDocument<TData>
        where TGetDocumentQuery : GetDocumentQuery<TData, TDocument>
    {
        #region Properties

        public abstract ServiceErrorCodes ServiceErrorCodes { get; }

        #endregion Properties

        #region Members

        protected readonly ILogger _logger;

        #endregion Members

        #region Ctor

        public GetDocumentQueryHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
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

        public abstract Task<TDocument> Handle(TGetDocumentQuery query, CancellationToken cancellationToken);

        protected virtual void OnBeginHandle(TGetDocumentQuery query) { }

        protected abstract Task<TDocument> GetDocument(Guid id);

        protected virtual void OnHandleComplete(TGetDocumentQuery query, TDocument queriedDocument) { }

        #endregion Methods
    }
}
