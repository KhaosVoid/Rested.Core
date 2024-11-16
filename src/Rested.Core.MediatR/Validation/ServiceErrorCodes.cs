namespace Rested.Core.MediatR.Validation;

public class ServiceErrorCodes : ServiceErrorCodesDictionary
{
    #region Properties

    public virtual CommonServiceErrorCodes CommonErrorCodes { get; protected set; }

    #endregion Properties

    #region Ctor

    public ServiceErrorCodes(int serviceId, int featureId) : base(serviceId, featureId)
    {
        OnInitializeCommonErrorCodes();
        OnInitializeCommonErrorCodesComplete();
    }

    #endregion Ctor

    #region Methods

    protected virtual void OnInitializeCommonErrorCodes()
    {
        CommonErrorCodes = new CommonServiceErrorCodes(_serviceId, _featureId);

        _errorCodes = CommonErrorCodes._errorCodes;
    }

    protected virtual void OnInitializeCommonErrorCodesComplete() { }

    #endregion Methods
}