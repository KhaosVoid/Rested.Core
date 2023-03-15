using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.Queries.Validators
{
    public class DateTimeFieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public DateTimeFieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            var filterValueIsRequiredErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired;
            var filterValueTypeIsInvalidErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid;
            var filterOperationNotSupportedErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported;
            var filterToValueIsRequiredErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired;
            var filterToValueTypeIsInvalidErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid;

            When(
                predicate: fieldFilterInfo => (DateTimeFieldFilterOperations)fieldFilterInfo.FilterOperation
                    is not DateTimeFieldFilterOperations.Blank
                    and not DateTimeFieldFilterOperations.NotBlank
                    and not DateTimeFieldFilterOperations.Empty,
                action: () =>
                {
                    RuleFor(fieldFilterInfo => fieldFilterInfo.FilterValue)
                        .NotEmpty()
                        .WithMessage(filterValueIsRequiredErrorCode.Message)
                        .WithErrorCode(filterValueIsRequiredErrorCode.ExtendedStatusCode);

                    When(
                        predicate: fieldFilterInfo => !string.IsNullOrWhiteSpace(fieldFilterInfo.FilterValue),
                        action: () =>
                        {
                            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterValue)
                                .Must(filterValue => DateTimeOffset.TryParse(filterValue, out var _))
                                .WithMessage(v => string.Format(filterValueTypeIsInvalidErrorCode.Message, v.FilterType))
                                .WithErrorCode(filterValueTypeIsInvalidErrorCode.ExtendedStatusCode);
                        });
                });

            RuleFor(fieldFilterInfo => (DateTimeFieldFilterOperations)fieldFilterInfo.FilterOperation)
                .IsInEnum()
                .WithMessage(filterOperationNotSupportedErrorCode.Message)
                .WithErrorCode(filterOperationNotSupportedErrorCode.ExtendedStatusCode);

            When(
                predicate: fieldFilterInfo => (DateTimeFieldFilterOperations)fieldFilterInfo.FilterOperation is DateTimeFieldFilterOperations.InRange,
                action: () =>
                {
                    RuleFor(fieldFilterInfo => fieldFilterInfo.FilterToValue)
                        .NotEmpty()
                        .WithMessage(filterToValueIsRequiredErrorCode.Message)
                        .WithErrorCode(filterToValueIsRequiredErrorCode.ExtendedStatusCode);

                    When(
                        predicate: fieldFilterInfo => !string.IsNullOrWhiteSpace(fieldFilterInfo.FilterToValue),
                        action: () =>
                        {
                            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterToValue)
                                .Must(filterToValue => DateTimeOffset.TryParse(filterToValue, out var _))
                                .WithMessage(v => string.Format(filterToValueTypeIsInvalidErrorCode.Message, v.FilterType))
                                .WithErrorCode(filterToValueTypeIsInvalidErrorCode.ExtendedStatusCode);
                        });
                });
        }
    }
}
