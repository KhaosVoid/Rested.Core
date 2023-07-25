using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.MediatR.Validation
{
    /// <summary>
    /// Factory class used to throw a <see cref="ValidationException"/> for a property name and value using an <see cref="ErrorCode.StatusCode"/> and <see cref="ErrorCode.Message"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ValidationExceptionFactory
    {
        /// <summary>
        /// Throws a <see cref="ValidationException"/> using the specified <see cref="ServiceErrorCode"/> as well as the property name and property value.
        /// </summary>
        /// <param name="serviceErrorCode">The service error code.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <exception cref="ValidationException"></exception>
        public static void Throw(ServiceErrorCode serviceErrorCode, string propertyName = null, string propertyValue = null)
        {
            var validationErrors = new List<ValidationFailure>()
            {
                new ValidationFailure(propertyName, serviceErrorCode.Message, propertyValue)
                {
                    ErrorCode = serviceErrorCode.ExtendedStatusCode,
                }
            };

            throw new ValidationException(validationErrors);
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> using the specified <see cref="ServiceErrorCode"/> as well as the property name, property value, and supplied message arguments.
        /// </summary>
        /// <param name="serviceErrorCode">The service error code.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void Throw(ServiceErrorCode serviceErrorCode, string propertyName = null, string propertyValue = null, params object[] messageArgs)
        {
            var validationErrors = new List<ValidationFailure>()
            {
                new ValidationFailure(propertyName, string.Format(serviceErrorCode.Message, messageArgs), propertyValue)
                {
                    ErrorCode = serviceErrorCode.ExtendedStatusCode,
                }
            };

            throw new ValidationException(validationErrors);
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the condition is true, using the specified <see cref="ServiceErrorCode"/> as well as the property name and property value.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="serviceErrorCode">The service error code.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void ThrowIf(Func<bool> condition, ServiceErrorCode serviceErrorCode, string propertyName = null, string propertyValue = null)
        {
            if (condition())
            {
                var validationErrors = new List<ValidationFailure>()
                {
                    new ValidationFailure(propertyName, serviceErrorCode.Message, propertyValue)
                    {
                        ErrorCode = serviceErrorCode.ExtendedStatusCode,
                    }
                };

                throw new ValidationException(validationErrors);
            }
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the condition is true, using the specified <see cref="ServiceErrorCode"/> as well as the property name, property value, and supplied message arguments.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="serviceErrorCode">The service error code.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public static void ThrowIf(Func<bool> condition, ServiceErrorCode serviceErrorCode, string propertyName = null, string propertyValue = null, params object[] messageArgs)
        {
            if (condition())
            {
                var validationErrors = new List<ValidationFailure>()
                {
                    new ValidationFailure(propertyName, string.Format(serviceErrorCode.Message, messageArgs), propertyValue)
                    {
                        ErrorCode = serviceErrorCode.ExtendedStatusCode,
                    }
                };

                throw new ValidationException(validationErrors);
            }
        }
    }
}
