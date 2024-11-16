namespace Rested.Core.MediatR.Validation;

public abstract class ErrorCode
{
    #region Properties

    public string? Name { get; internal set; }
    public string? Message { get; internal set; }

    #endregion Properties

    #region Ctor

    internal ErrorCode()
    {

    }

    public ErrorCode(string name, string message)
    {
        Name = name;
        Message = message;
    }

    #endregion Ctor

    #region Methods

    public static TErrorCode CreateUndefinedErrorCode<TErrorCode>(string errorCodeName) where TErrorCode : ErrorCode, new()
    {
        return new TErrorCode()
        {
            Name = errorCodeName,
            Message = $"The '{errorCodeName}' error code was not found."
        };
    }

    #endregion Methods
}