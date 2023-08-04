using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedGetDocumentRouteAttributeTest : RestedRouteAttributeTest<RestedGetDocumentRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.SingleResourceWithIdMethodRouteTemplate;
    }
}
