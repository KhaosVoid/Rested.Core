using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class FiltersValidator : AbstractValidator<List<IFilter>>
{
    public FiltersValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
    {
        RuleForEach(filters => filters)
            .SetInheritanceValidator(v =>
            {
                v.Add(_ => new TextFieldFilterValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
                v.Add(_ => new NumberFieldFilterValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
                v.Add(_ => new DateFieldFilterValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
                v.Add(_ => new DateTimeFieldFilterValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
                v.Add(_ => new OperatorFilterValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
            })
            .When(filters => filters is not null && filters.Count > 0);
    }
}