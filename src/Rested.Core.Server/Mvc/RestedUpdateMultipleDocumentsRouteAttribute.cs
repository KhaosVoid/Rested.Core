using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies an UpdateMultipleDocuments route on a Rested controller.
    /// </summary>
    public class RestedUpdateMultipleDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
    {
        #region Ctor

        public RestedUpdateMultipleDocumentsRouteAttribute() : base()
        {

        }

        public RestedUpdateMultipleDocumentsRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
