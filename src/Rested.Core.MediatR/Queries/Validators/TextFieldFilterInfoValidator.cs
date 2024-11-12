using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Search;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Queries.Validators
{
    public class TextFieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public TextFieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            var filterValueIsRequiredErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired;
            var filterOperationNotSupportedErrorCode = serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported;

            When(
                predicate: m => (TextFieldFilterOperations)m.FilterOperation
                    is not TextFieldFilterOperations.Blank
                    and not TextFieldFilterOperations.NotBlank
                    and not TextFieldFilterOperations.Empty,
                action: () =>
                {
                    RuleFor(m => m.FilterValue)
                        .NotNull()
                        .WithMessage(filterValueIsRequiredErrorCode.Message)
                        .WithErrorCode(filterValueIsRequiredErrorCode.ExtendedStatusCode);
                });

            RuleFor(m => (TextFieldFilterOperations)m.FilterOperation)
                .IsInEnum()
                .WithMessage(filterOperationNotSupportedErrorCode.Message)
                .WithErrorCode(filterOperationNotSupportedErrorCode.ExtendedStatusCode);
        }
    }
}
