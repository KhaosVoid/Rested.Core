using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies a PatchMultipleDocuments route on a Rested controller.
/// </summary>
public class RestedPatchMultipleDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
{
    #region Ctor

    public RestedPatchMultipleDocumentsRouteAttribute() : base()
    {

    }

    public RestedPatchMultipleDocumentsRouteAttribute(
        [StringSyntax("Route")] string template = null,
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}