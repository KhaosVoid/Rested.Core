using FluentValidation;
using Rested.Core.Data;
using Rested.Core.MediatR.Validation;
using System.Text.RegularExpressions;
using Rested.Core.Data.Search;

namespace Rested.Core.MediatR.Queries.Validators
{
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
                        .Must(m => IsFieldNameValid(m.FieldName, validFieldNames, ignoredFieldNames))
                        .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.SortingFieldNameIsInvalid);
                });
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
