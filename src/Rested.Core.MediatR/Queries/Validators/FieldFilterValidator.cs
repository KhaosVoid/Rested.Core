using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public abstract class FieldFilterValidator<TFieldFilter> : AbstractValidator<TFieldFilter>
    where TFieldFilter : IFieldFilter
{
    protected FieldFilterValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
    {
        RuleFor(fieldFilter => fieldFilter.FieldName)
            .NotEmpty()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterNameIsRequired);

        When(
            predicate: fieldFilter => !string.IsNullOrWhiteSpace(fieldFilter.FieldName),
            action: () =>
            {
                RuleFor(fieldFilter => fieldFilter)
                    .Must(fieldFilter => DataUtility.IsFieldNameValid(fieldFilter.FieldName, validFieldNames, ignoredFieldNames))
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterNameIsInvalid);
            });
    }
}