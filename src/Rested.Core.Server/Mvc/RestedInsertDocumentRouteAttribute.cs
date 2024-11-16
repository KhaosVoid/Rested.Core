using System.Diagnostics.CodeAnalysis;

namespace Rested.Core.Server.Mvc;

/// <summary>
/// Specifies an InsertDocument route on a Rested controller.
/// </summary>
public class RestedInsertDocumentRouteAttribute : RestedSingleResourceRouteAttribute
{
    #region Ctor

    public RestedInsertDocumentRouteAttribute() : base()
    {

    }

    public RestedInsertDocumentRouteAttribute(
        [StringSyntax("Route")] string template = null,
        bool overridesConfig = false) :
        base(template, overridesConfig)
    {

    }

    #endregion Ctor
}