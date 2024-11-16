using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedInsertDocumentRouteAttributeTest : RestedRouteAttributeTest<RestedInsertDocumentRouteAttribute>
{
    protected override string OnSetExpectedRouteTemplate() =>
        TestRestedRouteTemplateSettings.SingleResourceMethodRouteTemplate;
}