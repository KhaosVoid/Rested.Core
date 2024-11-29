using FluentValidation;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators;

public class TextFieldFilterValidator : FieldFilterValidator<TextFieldFilter>
{
    public TextFieldFilterValidator(ValidFieldNameGenerator validFieldNameGenerator, ServiceErrorCodes serviceErrorCodes) :
        base(validFieldNameGenerator, serviceErrorCodes)
    {
        When(
            predicate: textFieldFilter => textFieldFilter.FilterOperation
                is not TextFieldFilterOperations.Blank
                and not TextFieldFilterOperations.NotBlank
                and not TextFieldFilterOperations.Empty,
            action: () =>
            {
                RuleFor(textFieldFilter => textFieldFilter.Value)
                    .NotNull()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
            });
            
        RuleFor(filter => filter.FilterOperation)
            .IsInEnum()
            .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
    }
}