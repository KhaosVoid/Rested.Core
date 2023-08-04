using Microsoft.AspNetCore.Mvc;
using Rested.Core.Server.Data;
using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    public class RestedSingleResourceWithIdRouteAttribute : RouteAttribute
    {
        #region Ctor

        public RestedSingleResourceWithIdRouteAttribute() :
            base(RestedRouteTemplateSettings.CalculateSingleResourceWithIdRouteTemplate())
        {

        }

        public RestedSingleResourceWithIdRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(RestedRouteTemplateSettings.CalculateSingleResourceWithIdRouteTemplate(template, overridesConfig))
        {

        }

        #endregion Ctor
    }
}
