using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a GetProjection route on a Rested controller.
    /// </summary>
    public class RestedGetProjectionRouteAttribute : RestedSingleResourceWithIdRouteAttribute
    {
        #region Ctor

        public RestedGetProjectionRouteAttribute() : base()
        {

        }

        public RestedGetProjectionRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
