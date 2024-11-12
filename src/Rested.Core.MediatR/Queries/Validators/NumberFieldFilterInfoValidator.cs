using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators
{
    public class NumberFieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public NumberFieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            When(
                predicate: fieldFilterInfo => (NumberFieldFilterOperations)fieldFilterInfo.FilterOperation
                    is not NumberFieldFilterOperations.Blank
                    and not NumberFieldFilterOperations.NotBlank
                    and not NumberFieldFilterOperations.Empty,
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
                                .Must(filterValue => int.TryParse(filterValue, out var _))
                                .WithServiceErrorCode(
                                    serviceErrorCode: serviceErrorCodes.CommonErrorCodes.FieldFilterValueTypeIsInvalid,
                                    messageArgsProvider: fieldFilterInfo => new object[] { fieldFilterInfo.FilterType });
                        });
                });

            RuleFor(fieldFilterInfo => (NumberFieldFilterOperations)fieldFilterInfo.FilterOperation)
                .IsInEnum()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

            When(
                predicate: fieldFilterInfo => (NumberFieldFilterOperations)fieldFilterInfo.FilterOperation is NumberFieldFilterOperations.InRange,
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
                                .Must(filterTo => int.TryParse(filterTo, out var _))
                                .WithServiceErrorCode(
                                    serviceErrorCode: serviceErrorCodes.CommonErrorCodes.FieldFilterToValueTypeIsInvalid,
                                    messageArgsProvider: fieldFilterInfo => new object[] { fieldFilterInfo.FilterType });
                        });
                });
        }
    }
}
