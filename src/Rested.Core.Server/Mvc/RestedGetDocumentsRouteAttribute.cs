using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies a GetDocuments route on a Rested controller.
/// </summary>
public class RestedGetDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
{
    #region Ctor

    public RestedGetDocumentsRouteAttribute() : base()
    {

    }

    public RestedGetDocumentsRouteAttribute(
        [StringSyntax("Route")] string template = null,
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}