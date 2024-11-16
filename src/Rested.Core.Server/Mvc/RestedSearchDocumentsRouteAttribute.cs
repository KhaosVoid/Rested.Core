using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies a SearchDocuments route on a Rested controller.
/// </summary>
public class RestedSearchDocumentsRouteAttribute : RestedMultiResourceRouteAttribute
{
    #region Ctor

    public RestedSearchDocumentsRouteAttribute() : base(template: @"/search")
    {

    }

    public RestedSearchDocumentsRouteAttribute(
        [StringSyntax("Route")] string template = @"/search",
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}