using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class FieldSortInfoValidator : AbstractValidator<FieldSortInfo>
{
    public FieldSortInfoValidator(ValidFieldNameGenerator validFieldNameGenerator, ServiceErrorCodes serviceErrorCodes)
    {
        RuleFor(fieldSortInfo => fieldSortInfo.FieldName)
            .NotEmpty()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.SortingFieldNameIsRequired);

        When(
            predicate: fieldSortInfo => !string.IsNullOrWhiteSpace(fieldSortInfo.FieldName),
            action: () =>
            {
                RuleFor(fieldSortInfo => fieldSortInfo)
                    .Must(fieldSortInfo => validFieldNameGenerator.IsFieldNameValid(fieldSortInfo.FieldName))
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.SortingFieldNameIsInvalid);
            });
    }
}