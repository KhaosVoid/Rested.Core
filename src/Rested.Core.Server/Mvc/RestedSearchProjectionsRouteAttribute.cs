using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies a SearchProjections route on a Rested controller.
/// </summary>
public class RestedSearchProjectionsRouteAttribute : RestedMultiResourceRouteAttribute
{
    #region Ctor

    public RestedSearchProjectionsRouteAttribute() : base(template: @"/search")
    {

    }

    public RestedSearchProjectionsRouteAttribute(
        [StringSyntax("Route")] string template = @"/search",
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}