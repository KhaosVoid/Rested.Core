using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries;

public abstract class GetDocumentsQuery<TData, TDocument> : IQuery<List<TDocument>>
    where TData : IData
    where TDocument : IDocument<TData>
{
    #region Ctor

    public GetDocumentsQuery()
    {

    }

    #endregion Ctor
}

public abstract class GetDocumentsQueryValidator<TData, TDocument, TGetDocumentsQuery> : AbstractValidator<TGetDocumentsQuery>, IQueryValidator
    where TData : IData
    where TDocument : IDocument<TData>
    where TGetDocumentsQuery : GetDocumentsQuery<TData, TDocument>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Ctor

    public GetDocumentsQueryValidator()
    {

    }

    #endregion Ctor
}

public abstract class GetDocumentsQueryHandler<TData, TDocument, TGetDocumentsQuery> : IQueryHandler<List<TDocument>, TGetDocumentsQuery>
    where TData : IData
    where TDocument : IDocument<TData>
    where TGetDocumentsQuery : GetDocumentsQuery<TData, TDocument>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Members

    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public GetDocumentsQueryHandler(ILoggerFactory loggerFactory)
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

    public abstract Task<List<TDocument>> Handle(TGetDocumentsQuery query, CancellationToken cancellationToken);

    protected virtual void OnBeginHandle(TGetDocumentsQuery query) { }

    protected abstract Task<List<TDocument>> GetDocuments();

    protected virtual void OnHandleComplete(TGetDocumentsQuery query, IEnumerable<TDocument> queriedDocuments) { }

    #endregion Methods
}