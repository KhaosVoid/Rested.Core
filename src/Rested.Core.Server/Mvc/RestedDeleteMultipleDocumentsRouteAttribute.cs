using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a DeleteMultipleDocuments route on a Rested controller.
    /// </summary>
    public class RestedDeleteMultipleDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
    {
        #region Ctor

        public RestedDeleteMultipleDocumentsRouteAttribute() : base(template: @"/delete")
        {

        }

        public RestedDeleteMultipleDocumentsRouteAttribute(
            [StringSyntax("Route")] string template = @"/delete",
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
