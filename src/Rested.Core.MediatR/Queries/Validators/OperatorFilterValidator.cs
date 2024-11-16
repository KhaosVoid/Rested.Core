using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class OperatorFilterValidator : AbstractValidator<OperatorFilter>
{
    public OperatorFilterValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
    {
        RuleFor(operatorFilter => operatorFilter.Operator)
            .IsInEnum()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

        RuleFor(operatorFilter => operatorFilter.Filters)
            .NotEmpty()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.OperatorFilterFiltersIsRequired);

        RuleFor(operatorFilter => operatorFilter.Filters)
            .SetValidator(_ => new FiltersValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes))
            .When(operatorFilter => operatorFilter.Filters is not null && operatorFilter.Filters.Count > 0);
    }
}