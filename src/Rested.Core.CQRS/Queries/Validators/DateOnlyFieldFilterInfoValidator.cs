using FluentValidation;
using Rested.Core.CQRS.Data;
using Rested.Core.CQRS.Validation;

namespace Rested.Core.CQRS.Queries.Validators
{
    public class DateOnlyFieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public DateOnlyFieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            When(
                predicate: fieldFilterInfo => (DateOnlyFieldFilterOperations)fieldFilterInfo.FilterOperation
                    is not DateOnlyFieldFilterOperations.Blank
                    and not DateOnlyFieldFilterOperations.NotBlank
                    and not DateOnlyFieldFilterOperations.Empty,
                action: () =>
                {
                    RuleFor(fieldFilterInfo => fieldFilterInfo.FilterValue)
                        .NotEmpty()
                        .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);

                    When(
                        predicate: fieldFilterInfo => !string.IsNullOrWhiteSpace(fieldFilterInfo.FilterValue),
                        action: () =>
                        {
                            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterValue)
                                .Must(filterValue => DateOnly.TryParse(filterValue, out var _))
                                .WithServiceErrorCode(
                                    serviceErrorCode: serviceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid,
                                    messageArgsProvider: fieldFilterInfo => new object[] { fieldFilterInfo.FilterType });
                        });
                });

            RuleFor(fieldFilterInfo => (DateOnlyFieldFilterOperations)fieldFilterInfo.FilterOperation)
                .IsInEnum()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

            When(
                predicate: fieldFilterInfo => (DateOnlyFieldFilterOperations)fieldFilterInfo.FilterOperation is DateOnlyFieldFilterOperations.InRange,
                action: () =>
                {
                    RuleFor(fieldFilterInfo => fieldFilterInfo.FilterToValue)
                        .NotEmpty()
                        .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);

                    When(
                        predicate: fieldFilterInfo => !string.IsNullOrWhiteSpace(fieldFilterInfo.FilterToValue),
                        action: () =>
                        {
                            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterToValue)
                                .Must(filterToValue => DateOnly.TryParse(filterToValue, out var _))
                                .WithServiceErrorCode(
                                    serviceErrorCode: serviceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid,
                                    messageArgsProvider: fieldFilterInfo => new object[] { fieldFilterInfo.FilterType });
                        });
                });
        }
    }
}
