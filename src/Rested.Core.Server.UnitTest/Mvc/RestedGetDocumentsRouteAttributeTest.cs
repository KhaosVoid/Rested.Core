using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedGetDocumentsRouteAttributeTest : RestedRouteAttributeTest<RestedGetDocumentsRouteAttribute>
{
    protected override string OnSetExpectedRouteTemplate() =>
        TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate;
}