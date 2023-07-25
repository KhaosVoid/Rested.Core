using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;

namespace Rested.Core.Server.Http
{
    public class IfMatchByteArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(
                argument: bindingContext,
                paramName: nameof(bindingContext));

            var headers = bindingContext.ActionContext.HttpContext.Request.Headers;

            if (headers.ContainsKey(HeaderNames.IfMatch))
            {
                try
                {
                    var model = new IfMatchByteArray(
                        tag: Convert.FromBase64String(headers[HeaderNames.IfMatch]));

                    bindingContext.Result = ModelBindingResult.Success(model);

                    return Task.CompletedTask;
                }
                catch (Exception)
                {
                    bindingContext.ModelState.SetModelValue(
                        key: HeaderNames.IfMatch,
                        rawValue: headers[HeaderNames.IfMatch],
                        attemptedValue: headers[HeaderNames.IfMatch]);

                    bindingContext.ModelState.TryAddModelError(
                        key: HeaderNames.IfMatch,
                        errorMessage: $"{HeaderNames.IfMatch} header could not be converted from Base64.");

                    bindingContext.Result = ModelBindingResult.Failed();

                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }
}
