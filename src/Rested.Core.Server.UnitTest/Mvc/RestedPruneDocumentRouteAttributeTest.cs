using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedPruneDocumentRouteAttributeTest : RestedRouteAttributeTest<RestedPruneDocumentRouteAttribute>
    {
        protected override string OnSetDefaultTemplateParameter() => @"/prune";

        protected override string OnSetExpectedRouteTemplate() =>
            $@"{TestRestedRouteTemplateSettings.SingleResourceWithIdMethodRouteTemplate}{_defaultTemplateParameter}";
    }
}
