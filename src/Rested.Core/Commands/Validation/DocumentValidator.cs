using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.Commands.Validation
{
    public class DocumentValidator<TData, TDocument> : AbstractValidator<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        public DocumentValidator(CommandActions action, ServiceErrorCodes serviceErrorCodes)
        {
            if (action is CommandActions.Update or CommandActions.Patch or CommandActions.Delete)
            {
                RuleFor(document => document.Id)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.IDIsRequired);

                RuleFor(document => document.ETag)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.ETagIsRequired);
            }
        }
    }
}
