using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedInsertMultipleDocumentsRouteAttributeTest : RestedRouteAttributeTest<RestedInsertMultipleDocumentsRouteAttribute>
    {
        protected override string OnSetExpectedRouteTemplate() =>
            TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate;
    }
}
