using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedApiControllerRouteAttributeTest : RestedRouteAttributeTest<RestedApiControllerRouteAttribute>
{
    protected override string OnSetExpectedRouteTemplate() =>
        TestRestedRouteTemplateSettings.ControllerRouteTemplate;
}