using System.Net;

namespace Rested.Core.CQRS.Validation
{
    public abstract class ServiceErrorCodesDictionary
    {
        #region Properties

        public int Count => _errorCodes.Count;

        #endregion Properties

        #region Indexer

        public ServiceErrorCode this[string errorCodeName] => _errorCodes[errorCodeName];

        #endregion Indexer

        #region Members

        protected readonly int _serviceId;
        protected readonly int _featureId;

        internal Dictionary<string, ServiceErrorCode> _errorCodes;

        #endregion Members

        #region Ctor

        public ServiceErrorCodesDictionary(int serviceId, int featureId)
        {
            _serviceId = serviceId;
            _featureId = featureId;
            _errorCodes = new Dictionary<string, ServiceErrorCode>();
        }

        #endregion Ctor

        #region Methods

        public void Add(string name, string message, HttpStatusCode httpStatusCode)
        {
            var nextFailureCode = 1;

            if (_errorCodes.Count > 1)
                nextFailureCode = _errorCodes.Values.Max(serviceErrorCode => serviceErrorCode.FailureCode) + 1;

            Add(name, message, httpStatusCode, nextFailureCode);
        }

        public void Add(string name, string message, HttpStatusCode httpStatusCode, int failureCode)
        {
            _errorCodes.Add(
                key: name,
                value: new ServiceErrorCode(
                    name: name,
                    message: message,
                    httpStatusCode: httpStatusCode,
                    serviceId: _serviceId,
                    featureId: _featureId,
                    failureCode: failureCode));
        }

        #endregion Methods
    }
}
