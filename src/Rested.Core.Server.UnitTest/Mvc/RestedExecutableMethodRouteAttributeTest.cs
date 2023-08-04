using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedExecutableMethodRouteAttributeTest : RestedRouteAttributeTest<RestedExecutableMethodRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.SingleResourceMethodRouteTemplate;
    }
}
