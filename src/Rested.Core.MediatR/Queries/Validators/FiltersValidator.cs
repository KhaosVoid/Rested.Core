using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class FiltersValidator : AbstractValidator<List<IFilter>>
{
    public FiltersValidator(ValidFieldNameGenerator validFieldNameGenerator, ServiceErrorCodes serviceErrorCodes)
    {
        RuleForEach(filters => filters)
            .SetInheritanceValidator(v =>
            {
                v.Add(_ => new TextFieldFilterValidator(validFieldNameGenerator, serviceErrorCodes));
                v.Add(_ => new NumberFieldFilterValidator(validFieldNameGenerator, serviceErrorCodes));
                v.Add(_ => new DateFieldFilterValidator(validFieldNameGenerator, serviceErrorCodes));
                v.Add(_ => new DateTimeFieldFilterValidator(validFieldNameGenerator, serviceErrorCodes));
                v.Add(_ => new OperatorFilterValidator(validFieldNameGenerator, serviceErrorCodes));
            })
            .When(filters => filters is not null && filters.Count > 0);
    }
}