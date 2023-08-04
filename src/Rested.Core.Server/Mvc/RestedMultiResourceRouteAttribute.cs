using Microsoft.AspNetCore.Mvc;
using Rested.Core.Server.Data;
using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    public class RestedMultiResourceRouteAttribute : RouteAttribute
    {
        #region Ctor

        public RestedMultiResourceRouteAttribute() :
            base(RestedRouteTemplateSettings.CalculateMultiResourceRouteTemplate())
        {

        }

        public RestedMultiResourceRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(RestedRouteTemplateSettings.CalculateMultiResourceRouteTemplate(template, overridesConfig))
        {

        }

        #endregion Ctor
    }
}
