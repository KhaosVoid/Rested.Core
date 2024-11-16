using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies a GetProjections route on a Rested controller.
/// </summary>
public class RestedGetProjectionsRouteAttribute : RestedMultiResourceRouteAttribute
{
    #region Ctor

    public RestedGetProjectionsRouteAttribute() : base()
    {

    }

    public RestedGetProjectionsRouteAttribute(
        [StringSyntax("Route")] string template = null,
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}