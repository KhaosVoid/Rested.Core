namespace Rested.Core.CQRS.Data
{
    public interface IBaseDto : IIdentifiable
    {
        byte[] ETag { get; set; }
    }
}
