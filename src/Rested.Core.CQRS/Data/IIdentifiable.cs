namespace Rested.Core.CQRS.Data
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
