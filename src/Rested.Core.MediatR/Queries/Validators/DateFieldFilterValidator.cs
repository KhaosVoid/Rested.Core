using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class DateFieldFilterValidator : FieldFilterValidator<DateFieldFilter>
{
    public DateFieldFilterValidator(ValidFieldNameGenerator validFieldNameGenerator, ServiceErrorCodes serviceErrorCodes) :
        base(validFieldNameGenerator, serviceErrorCodes)
    {
        When(
            predicate: dateFieldFilter => dateFieldFilter.FilterOperation
                is not DateFieldFilterOperations.Blank
                and not DateFieldFilterOperations.NotBlank
                and not DateFieldFilterOperations.Empty,
            action: () =>
            {
                RuleFor(dateFieldFilter => dateFieldFilter.Value)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
            });

        RuleFor(dateFieldFilter => dateFieldFilter.FilterOperation)
            .IsInEnum()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

        When(
            predicate: dateFieldFilter => dateFieldFilter.FilterOperation is DateFieldFilterOperations.InRange,
            action: () =>
            {
                RuleFor(dateFieldFilter => dateFieldFilter.ToValue)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
            });
    }
}