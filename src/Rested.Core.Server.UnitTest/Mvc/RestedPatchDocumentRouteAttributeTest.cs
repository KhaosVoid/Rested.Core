using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedPatchDocumentRouteAttributeTest : RestedRouteAttributeTest<RestedPatchDocumentRouteAttribute>
{
    protected override string OnSetExpectedRouteTemplate() =>
        TestRestedRouteTemplateSettings.SingleResourceWithIdMethodRouteTemplate;
}