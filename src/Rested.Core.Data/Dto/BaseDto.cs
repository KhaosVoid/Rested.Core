namespace Rested.Core.Data.Dto
{
    public record BaseDto : IBaseDto
    {
        #region Properties

        public Guid Id { get; set; }
        public byte[] ETag { get; set; }

        #endregion Properties

        #region Methods

        public static BaseDto FromDto<TData>(Dto<TData> dto) where TData : IData
        {
            return new BaseDto()
            {
                Id = dto.Id,
                ETag = dto.ETag
            };
        }

        public static List<BaseDto> ToList<TData>(IEnumerable<Dto<TData>> dtoList) where TData : IData
        {
            return dtoList?
                .Select(FromDto)
                .ToList();
        }

        #endregion Methods
    }
}
