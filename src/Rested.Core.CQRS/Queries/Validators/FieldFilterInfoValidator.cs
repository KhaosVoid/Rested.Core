using FluentValidation;
using Rested.Core.CQRS.Data;
using Rested.Core.CQRS.Validation;
using System.Text.RegularExpressions;

namespace Rested.Core.CQRS.Queries.Validators
{
    public class FieldFilterInfoValidator : AbstractValidator<FieldFilterInfo>
    {
        public FieldFilterInfoValidator(IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames, ServiceErrorCodes serviceErrorCodes)
        {
            RuleFor(fieldFilterInfo => fieldFilterInfo.FieldName)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterNameIsRequired);

            When(
                predicate: fieldFilterInfo => !string.IsNullOrWhiteSpace(fieldFilterInfo.FieldName),
                action: () =>
                {
                    RuleFor(fieldFilterInfo => fieldFilterInfo)
                        .Must(fieldFilterInfo => IsFieldNameValid(fieldFilterInfo.FieldName, validFieldNames, ignoredFieldNames))
                        .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.FieldFilterNameIsInvalid);
                });

            When(
                predicate: fieldFilterInfo => fieldFilterInfo.FilterType is FieldFilterTypes.Text,
                action: () => Include(v => new TextFieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes)));

            When(
                predicate: fieldFilterInfo => fieldFilterInfo.FilterType is FieldFilterTypes.Number,
                action: () => Include(v => new NumberFieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes)));

            When(
                predicate: fieldFilterInfo => fieldFilterInfo.FilterType is FieldFilterTypes.Date,
                action: () => Include(v => new DateOnlyFieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes)));

            When(
                predicate: fieldFilterInfo => fieldFilterInfo.FilterType is FieldFilterTypes.DateTime,
                action: () => Include(v => new DateTimeFieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes)));

            When(
                predicate: fieldFilterInfo => fieldFilterInfo.FilterType is FieldFilterTypes.Combined,
                action: () => Include(v => new CombinedFieldFilterInfoValidator(validFieldNames, ignoredFieldNames, serviceErrorCodes)));
        }

        protected bool IsFieldNameValid(string fieldName, IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames)
        {
            fieldName = Regex.Replace(
                input: fieldName,
                pattern: @"\[.*?\]",
                replacement: "");

            bool doesFieldNameExist = validFieldNames.Contains(fieldName);
            bool isFieldNameIgnored = ignoredFieldNames.Contains(fieldName);

            return doesFieldNameExist && !isFieldNameIgnored;
        }
    }
}
