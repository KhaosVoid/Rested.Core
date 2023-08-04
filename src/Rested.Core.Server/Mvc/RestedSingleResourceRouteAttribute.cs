using Microsoft.AspNetCore.Mvc;
using Rested.Core.Server.Data;
using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    public class RestedSingleResourceRouteAttribute : RouteAttribute
    {
        #region Ctor

        public RestedSingleResourceRouteAttribute() :
            base(RestedRouteTemplateSettings.CalculateSingleResourceRouteTemplate())
        {

        }

        public RestedSingleResourceRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(RestedRouteTemplateSettings.CalculateSingleResourceRouteTemplate(template, overridesConfig))
        {

        }

        #endregion Ctor
    }
}
