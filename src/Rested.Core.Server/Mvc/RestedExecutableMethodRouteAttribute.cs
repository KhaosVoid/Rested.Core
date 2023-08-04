using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    public class RestedExecutableMethodRouteAttribute : RestedSingleResourceRouteAttribute
    {
        #region Ctor

        public RestedExecutableMethodRouteAttribute() : base()
        {

        }

        public RestedExecutableMethodRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
