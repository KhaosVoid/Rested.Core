using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a PatchDocument route on a Rested controller.
    /// </summary>
    public class RestedPatchDocumentRouteAttribute : RestedSingleResourceWithIdRouteAttribute
    {
        #region Ctor

        public RestedPatchDocumentRouteAttribute() : base()
        {

        }

        public RestedPatchDocumentRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
