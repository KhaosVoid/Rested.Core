using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedGetProjectionsRouteAttributeTest : RestedRouteAttributeTest<RestedGetProjectionsRouteAttribute>
{
    protected override string OnSetExpectedRouteTemplate() =>
        TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate;
}