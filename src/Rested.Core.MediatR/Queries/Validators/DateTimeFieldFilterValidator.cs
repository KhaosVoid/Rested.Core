using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class DateTimeFieldFilterValidator : FieldFilterValidator<DateTimeFieldFilter>
{
    public DateTimeFieldFilterValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes) :
        base(validFieldNames, ignoredFieldNames, serviceErrorCodes)
    {
        When(
            predicate: dateTimeFieldFilter => dateTimeFieldFilter.FilterOperation
                is not DateTimeFieldFilterOperations.Blank
                and not DateTimeFieldFilterOperations.NotBlank
                and not DateTimeFieldFilterOperations.Empty,
            action: () =>
            {
                RuleFor(dateTimeFieldFilter => dateTimeFieldFilter.Value)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
            });

        RuleFor(dateTimeFieldFilter => dateTimeFieldFilter.FilterOperation)
            .IsInEnum()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

        When(
            predicate: dateTimeFieldFilter => dateTimeFieldFilter.FilterOperation is DateTimeFieldFilterOperations.InRange,
            action: () =>
            {
                RuleFor(dateTimeFieldFilter => dateTimeFieldFilter.ToValue)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
            });
    }
}