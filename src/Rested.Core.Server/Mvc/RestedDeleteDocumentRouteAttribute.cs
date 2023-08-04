using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies a DeleteDocument route on a Rested controller.
    /// </summary>
    public class RestedDeleteDocumentRouteAttribute : RestedSingleResourceWithIdRouteAttribute
    {
        #region Ctor

        public RestedDeleteDocumentRouteAttribute() : base()
        {

        }

        public RestedDeleteDocumentRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
