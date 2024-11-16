using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.Data;
using Rested.Core.Data.Projection;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries;

public abstract class GetProjectionQuery<TData, TProjection> : IQuery<TProjection>
    where TData : IData
    where TProjection : Projection
{
    #region Properties

    public Guid Id { get; }

    #endregion Properties

    #region Ctor

    public GetProjectionQuery(Guid id)
    {
        Id = id;
    }

    #endregion Ctor
}

public abstract class GetProjectionQueryValidator<TData, TProjection, TGetProjectionQuery> : AbstractValidator<TGetProjectionQuery>, IQueryValidator
    where TData : IData
    where TProjection : Projection
    where TGetProjectionQuery : GetProjectionQuery<TData, TProjection>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Ctor

    public GetProjectionQueryValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithServiceErrorCode(ServiceErrorCodes.CommonErrorCodes.IDIsRequired);
    }

    #endregion Ctor
}

public abstract class GetProjectionQueryHandler<TData, TProjection, TGetProjectionQuery> : IQueryHandler<TProjection, TGetProjectionQuery>
    where TData : IData
    where TProjection : Projection
    where TGetProjectionQuery : GetProjectionQuery<TData, TProjection>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Members

    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public GetProjectionQueryHandler(ILoggerFactory loggerFactory)
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

    public abstract Task<TProjection> Handle(TGetProjectionQuery query, CancellationToken cancellationToken);

    protected virtual void OnBeginHandle(TGetProjectionQuery query) { }

    protected abstract Task<TProjection> GetProjection(Guid id);

    protected virtual void OnHandleComplete(TGetProjectionQuery query, TProjection queriedProjection) { }

    #endregion Methods
}