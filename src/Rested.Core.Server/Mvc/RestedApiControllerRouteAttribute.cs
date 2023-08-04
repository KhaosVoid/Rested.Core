using Microsoft.AspNetCore.Mvc;
using Rested.Core.Server.Data;
using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies an attribute route on a Rested Controller.
    /// </summary>
    public class RestedApiControllerRouteAttribute : RouteAttribute
    {
        #region Ctor

        public RestedApiControllerRouteAttribute() :
            base(RestedRouteTemplateSettings.CalculateControllerRouteTemplate())
        {

        }

        public RestedApiControllerRouteAttribute(
            [StringSyntax("Route")] string template,
            bool overridesConfig = false) :
                base(RestedRouteTemplateSettings.CalculateControllerRouteTemplate(template, overridesConfig))
        {

        }

        #endregion Ctor
    }
}
