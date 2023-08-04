using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedGetProjectionRouteAttributeTest : RestedRouteAttributeTest<RestedGetProjectionRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.SingleResourceWithIdMethodRouteTemplate;
    }
}
