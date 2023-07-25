using System.Net;

namespace Rested.Core.MediatR.Validation
{
    public class ServiceErrorCode : ErrorCode
    {
        #region Properties

        public HttpStatusCode HttpStatusCode { get; }
        public int ServiceId { get; }
        public int FeatureId { get; }
        public int FailureCode { get; }
        public string ExtendedStatusCode => $"{(int)HttpStatusCode}.{ServiceId}.{FeatureId}.{FailureCode}";

        #endregion Properties

        #region Ctor

        public ServiceErrorCode(string name, string message, HttpStatusCode httpStatusCode, int serviceId, int featureId, int failureCode) :
            base(name, message)
        {
            HttpStatusCode = httpStatusCode;
            ServiceId = serviceId;
            FeatureId = featureId;
            FailureCode = failureCode;
        }

        #endregion Ctor

        #region Methods

        public override string ToString() =>
            $"[{(int)HttpStatusCode}.{ServiceId}.{FeatureId}.{FailureCode}] - {Message}";

        #endregion Methods
    }
}
