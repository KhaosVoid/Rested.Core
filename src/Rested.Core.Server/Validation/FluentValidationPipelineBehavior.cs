using FluentValidation;
using MediatR;

namespace Rested.Core.Server.Validation
{
    public class FluentValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Members

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion Members

        #region Ctor

        public FluentValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion Ctor

        #region Methods

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (errors.Any())
                throw new ValidationException(errors);

            return await next();
        }

        #endregion Methods
    }
}
