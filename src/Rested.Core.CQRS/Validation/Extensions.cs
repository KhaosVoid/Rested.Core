using FluentValidation;

namespace Rested.Core.CQRS.Validation
{
    public static class Extensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithServiceErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            ServiceErrorCode serviceErrorCode)
        {
            ValidationExtensionGuard(
                obj: serviceErrorCode,
                paramName: nameof(serviceErrorCode),
                message: $"A {nameof(ServiceErrorCode)} must be specified when calling {nameof(WithServiceErrorCode)}.");

            return rule
                .WithErrorCode(serviceErrorCode.ExtendedStatusCode)
                .WithMessage(serviceErrorCode.Message);
        }

        public static IRuleBuilderOptions<T, TProperty> WithServiceErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            ServiceErrorCode serviceErrorCode,
            Func<T, object[]> messageArgsProvider)
        {
            ValidationExtensionGuard(
                obj: serviceErrorCode,
                paramName: nameof(serviceErrorCode),
                message: $"A {nameof(ServiceErrorCode)} must be specified when calling {nameof(WithServiceErrorCode)}.");

            return rule
                .WithErrorCode(serviceErrorCode.ExtendedStatusCode)
                .WithMessage(t => string.Format(serviceErrorCode.Message, messageArgsProvider(t)));
        }

        private static void ValidationExtensionGuard(object obj, string paramName, string message)
        {
            if (obj is null)
                throw new ArgumentNullException(paramName, message);

            if (obj is string str && str == string.Empty)
                throw new ArgumentException(message, paramName);
        }
    }
}
