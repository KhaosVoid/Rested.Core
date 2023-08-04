using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc
{
    /// <summary>
    /// Specifies an InsertMultipleDocuments route on a Rested controller.
    /// </summary>
    public class RestedInsertMultipleDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
    {
        #region Ctor

        public RestedInsertMultipleDocumentsRouteAttribute() : base()
        {

        }

        public RestedInsertMultipleDocumentsRouteAttribute(
            [StringSyntax("Route")] string template = null,
            bool overridesConfig = false) :
                base(template, overridesConfig)
        {

        }

        #endregion Ctor
    }
}
