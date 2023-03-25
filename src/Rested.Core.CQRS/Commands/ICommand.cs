using MediatR;
using Rested.Core.CQRS.Validation;

namespace Rested.Core.CQRS.Commands
{
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
}
