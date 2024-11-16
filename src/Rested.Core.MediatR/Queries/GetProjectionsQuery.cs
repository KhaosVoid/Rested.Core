using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Data.Projection;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries;

public abstract class GetProjectionsQuery<TData, TProjection> : IQuery<List<TProjection>>
    where TData : IData
    where TProjection : Projection
{
    #region Ctor

    public GetProjectionsQuery()
    {

    }

    #endregion Ctor
}

public abstract class GetProjectionsQueryValidator<TData, TProjection, TGetProjectionsQuery> : AbstractValidator<TGetProjectionsQuery>, IQueryValidator
    where TData : IData
    where TProjection : Projection
    where TGetProjectionsQuery : GetProjectionsQuery<TData, TProjection>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Ctor

    public GetProjectionsQueryValidator()
    {

    }

    #endregion Ctor
}

public abstract class GetProjectionsQueryHandler<TData, TProjection, TGetProjectionsQuery> : IQueryHandler<List<TProjection>, TGetProjectionsQuery>
    where TData : IData
    where TProjection : Projection
    where TGetProjectionsQuery : GetProjectionsQuery<TData, TProjection>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Members

    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public GetProjectionsQueryHandler(ILoggerFactory loggerFactory)
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

    public abstract Task<List<TProjection>> Handle(TGetProjectionsQuery query, CancellationToken cancellationToken);

    protected virtual void OnBeginHandle(TGetProjectionsQuery query) { }

    protected abstract Task<List<TProjection>> GetProjections();

    protected virtual void OnHandleComplete(TGetProjectionsQuery query, IEnumerable<TProjection> queriedProjections) { }

    #endregion Methods
}