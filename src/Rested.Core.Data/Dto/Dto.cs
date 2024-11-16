namespace Rested.Core.Data.Dto;

public record Dto<TData> : BaseDto where TData : IData
{
    #region Properties

    public TData Data { get; set; }

    #endregion Properties

    #region Methods

    public static Dto<TData> FromBaseDto(BaseDto baseDto)
    {
        return new Dto<TData>()
        {
            Id = baseDto.Id,
            ETag = baseDto.ETag
        };
    }

    public static Dto<TData> FromData(TData data)
    {
        return new Dto<TData>()
        {
            Data = data
        };
    }

    public static List<Dto<TData>> ToList(IEnumerable<BaseDto> baseDtoList)
    {
        return baseDtoList?
            .Select(baseDto => FromBaseDto(baseDto))
            .ToList();
    }

    public static List<Dto<TData>> ToList(IEnumerable<TData> dataList)
    {
        return dataList?
            .Select(data => FromData(data))
            .ToList();
    }

    #endregion Methods
}