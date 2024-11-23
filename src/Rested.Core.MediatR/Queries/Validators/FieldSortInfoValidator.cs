using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class FieldSortInfoValidator : AbstractValidator<FieldSortInfo>
{
    public FieldSortInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
    {
        RuleFor(fieldSortInfo => fieldSortInfo.FieldName)
            .NotEmpty()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.SortingFieldNameIsRequired);

        When(
            predicate: fieldSortInfo => !string.IsNullOrWhiteSpace(fieldSortInfo.FieldName),
            action: () =>
            {
                RuleFor(fieldSortInfo => fieldSortInfo)
                    .Must(m => DataUtility.IsFieldNameValid(m.FieldName, validFieldNames, ignoredFieldNames))
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.SortingFieldNameIsInvalid);
            });
    }
}