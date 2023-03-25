using FluentValidation;
using Rested.Core.CQRS.Data;
using Rested.Core.CQRS.Validation;

namespace Rested.Core.CQRS.Queries.Validators
{
    public class CombinedFieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public CombinedFieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            RuleFor(fieldFilterInfo => (CombinedFieldFilterOperations)fieldFilterInfo.FilterOperation)
                .IsInEnum()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);

            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterCondition1)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterFirstConditionIsRequired);

            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterCondition2)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterSecondConditionIsRequired);

            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterCondition1)
                .SetValidator(new FieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));

            RuleFor(fieldFilterInfo => fieldFilterInfo.FilterCondition2)
                .SetValidator(new FieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes));
        }
    }
}
