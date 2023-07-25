namespace Rested.Core.Data
{
    public interface IBaseDto : IIdentifiable
    {
        byte[] ETag { get; set; }
    }
}
