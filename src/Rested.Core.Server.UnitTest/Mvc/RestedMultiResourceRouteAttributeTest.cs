using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedMultiResourceRouteAttributeTest : RestedRouteAttributeTest<RestedMultiResourceRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate;
    }
}
