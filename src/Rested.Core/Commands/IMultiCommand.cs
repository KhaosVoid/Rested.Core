namespace Rested.Core.Commands
{
    public interface IMultiCommand<TResponseItem> : ICommand<List<TResponseItem>>
    {

    }

    public interface IMultiCommandValidator : ICommandValidator
    {

    }

    public interface IMultiCommandHandler<TResponseItem, TMultiCommand> : ICommandHandler<List<TResponseItem>, TMultiCommand>
        where TMultiCommand : IMultiCommand<TResponseItem>
    {

    }
}
