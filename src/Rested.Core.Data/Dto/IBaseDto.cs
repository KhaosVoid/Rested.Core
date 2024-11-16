namespace Rested.Core.Data.Dto;

public interface IBaseDto : IIdentifiable
{
    byte[] ETag { get; set; }
}