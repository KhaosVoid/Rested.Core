using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc;

[TestClass]
public class RestedDeleteMultipleDocumentsRouteAttributeTest : RestedRouteAttributeTest<RestedDeleteMultipleDocumentsRouteAttribute>
{
    protected override string OnSetDefaultTemplateParameter() => @"/delete";

    protected override string OnSetExpectedRouteTemplate() =>
        $@"{TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate}{_defaultTemplateParameter}";
}