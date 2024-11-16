using MediatR;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    CommandActions Action { get; }
}

public interface ICommandValidator
{
    ServiceErrorCodes ServiceErrorCodes { get; }
}

public interface ICommandHandler<TResponse, TCommand> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    ServiceErrorCodes ServiceErrorCodes { get; }
}