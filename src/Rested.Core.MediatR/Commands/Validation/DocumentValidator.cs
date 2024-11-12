using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Commands.Validation
{
    public class DocumentValidator<TData, TDocument> : AbstractValidator<TDocument>
        where TData : IData
        where TDocument : IDocument<TData>
    {
        public DocumentValidator(CommandActions action, ServiceErrorCodes serviceErrorCodes)
        {
            if (action is CommandActions.Update or CommandActions.Patch or CommandActions.Prune or CommandActions.Delete)
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
