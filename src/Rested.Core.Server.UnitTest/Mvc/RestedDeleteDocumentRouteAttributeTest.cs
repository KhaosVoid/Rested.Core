using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedDeleteDocumentRouteAttributeTest : RestedRouteAttributeTest<RestedDeleteDocumentRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.SingleResourceWithIdMethodRouteTemplate;
    }
}
