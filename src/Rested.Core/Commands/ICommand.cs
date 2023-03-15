using MediatR;
using Rested.Core.Validation;

namespace Rested.Core.Commands
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

        void CheckDependencies();
    }
}
