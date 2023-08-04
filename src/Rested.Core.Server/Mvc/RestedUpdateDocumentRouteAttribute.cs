using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies an UpdateDocument route on a Rested controller.
    /// </summary>
    public class RestedUpdateDocumentRouteAttribute : RestedSingleResourceWithIdRouteAttribute
    {
        #region Ctor

        public RestedUpdateDocumentRouteAttribute() : base()
        {

        }

        public RestedUpdateDocumentRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
