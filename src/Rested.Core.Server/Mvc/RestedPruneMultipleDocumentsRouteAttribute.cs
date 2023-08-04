using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a PruneMultipleDocuments route on a Rested controller.
    /// </summary>
    public class RestedPruneMultipleDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
    {
        #region Ctor

        public RestedPruneMultipleDocumentsRouteAttribute() : base(template: @"/prune")
        {

        }

        public RestedPruneMultipleDocumentsRouteAttribute(
            [StringSyntax("Route")] string template = @"/prune",
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
