using MediatR;
using Rested.Core.Validation;

namespace Rested.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }

    public interface IQueryValidator
    {
        ServiceErrorCodes ServiceErrorCodes { get; }
    }

    public interface IQueryHandler<TResponse, TQuery> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        ServiceErrorCodes ServiceErrorCodes { get; }

        void CheckDependencies();
    }
}
