using FluentValidation.Results;

namespace Rested.Core.CQRS.Validation
{
    public class ValidationError
    {
        #region Properties

        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }
        public object AttemptedValue { get; private set; }
        public string Severity { get; private set; }

        #endregion Properties

        #region Ctor

        private ValidationError()
        {

        }

        #endregion Ctor

        #region Methods

        public static ValidationError FromValidationFailure(ValidationFailure validationFailure)
        {
            return new ValidationError()
            {
                ErrorCode = validationFailure.ErrorCode,
                ErrorMessage = validationFailure.ErrorMessage,
                PropertyName = validationFailure.PropertyName,
                AttemptedValue = validationFailure.AttemptedValue,
                Severity = validationFailure.Severity.ToString()
            };
        }

        #endregion Methods
    }
}
