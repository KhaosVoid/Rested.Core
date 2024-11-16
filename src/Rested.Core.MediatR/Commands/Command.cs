using FluentValidation;
using Microsoft.Extensions.Logging;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Commands;

public abstract class Command<TResponse> : ICommand<TResponse>
{
    #region Properties

    public CommandActions Action { get; }

    #endregion Properties

    #region Ctor

    public Command(CommandActions action)
    {
        Action = action;
    }

    #endregion Ctor
}

public abstract class CommandValidator<TResponse, TCommand> : AbstractValidator<TCommand>, ICommandValidator
    where TCommand : Command<TResponse>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Ctor

    public CommandValidator()
    {

    }

    #endregion Ctor
}

public abstract class CommandHandler<TResponse, TCommand> : ICommandHandler<TResponse, TCommand>
    where TCommand : Command<TResponse>
{
    #region Properties

    public abstract ServiceErrorCodes ServiceErrorCodes { get; }

    #endregion Properties

    #region Members

    protected readonly ILogger _logger;

    #endregion Members

    #region Ctor

    public CommandHandler(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
    }

    #endregion Ctor

    #region Methods

    public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);

    #endregion Methods
}