using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class NumberFieldFilterValidator : FieldFilterValidator<NumberFieldFilter>
{
    public NumberFieldFilterValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes) :
        base(validFieldNames, ignoredFieldNames, serviceErrorCodes)
    {
        When(
            predicate: numberFieldFilter => numberFieldFilter.FilterOperation
                is not NumberFieldFilterOperations.Blank
                and not NumberFieldFilterOperations.NotBlank
                and not NumberFieldFilterOperations.Empty,
            action: () =>
            {
                RuleFor(numberFieldFilter => numberFieldFilter.Value)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
            });

        RuleFor(numberFieldFilter => numberFieldFilter.FilterOperation)
            .IsInEnum()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

        When(
            predicate: numberFieldFilter => numberFieldFilter.FilterOperation is NumberFieldFilterOperations.InRange,
            action: () =>
            {
                RuleFor(numberFieldFilter => numberFieldFilter.ToValue)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
            });
    }
}