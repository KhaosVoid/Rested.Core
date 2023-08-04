using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a PruneDocument route on a Rested controller.
    /// </summary>
    public class RestedPruneDocumentRouteAttribute : RestedSingleResourceWithIdRouteAttribute
    {
        #region Ctor

        public RestedPruneDocumentRouteAttribute() : base(template: @"/prune")
        {

        }

        public RestedPruneDocumentRouteAttribute(
            [StringSyntax("Route")] string template = @"/prune",
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
