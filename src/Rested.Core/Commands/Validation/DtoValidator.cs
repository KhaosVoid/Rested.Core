﻿using FluentValidation;
using Rested.Core.Data;
using Rested.Core.Validation;

namespace Rested.Core.Commands.Validation
{
    public class DtoValidator<TData> : AbstractValidator<Dto<TData>> where TData : IData
    {
        #region Ctor

        public DtoValidator(CommandActions action, ServiceErrorCodes serviceErrorCodes)
        {
            if (action is CommandActions.Insert or CommandActions.Update or CommandActions.Patch)
            {
                RuleFor(dto => dto.Data)
                    .NotEmpty()
                    .WithServiceErrorCode(serviceErrorCodes.CommonErrorCodes.DataIsRequired);
            }

            if (action is CommandActions.Update or CommandActions.Patch or CommandActions.Delete)
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
}