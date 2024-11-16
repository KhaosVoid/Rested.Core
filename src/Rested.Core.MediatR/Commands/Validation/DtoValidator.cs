using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Data.Dto;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.Commands.Validation;

public class DtoValidator<TData> : AbstractValidator<Dto<TData>> where TData : IData
{
    #region Ctor

    public DtoValidator(CommandActions action, ServiceErrorCodes serviceErrorCodes)
    {
        if (action is CommandActions.Insert or CommandActions.Update or CommandActions.Patch or CommandActions.Prune)
        {
            RuleFor(dto => dto.Data)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.DataIsRequired);
        }

        if (action is CommandActions.Update or CommandActions.Patch or CommandActions.Prune or CommandActions.Delete)
        {
            RuleFor(dto => dto.Id)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.IDIsRequired);

            RuleFor(dto => dto.ETag)
                .NotEmpty()
                .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.ETagIsRequired);
        }
    }

    #endregion Ctor
}