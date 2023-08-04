using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedPruneMultipleDocumentsRouteAttributeTest : RestedRouteAttributeTest<RestedPruneMultipleDocumentsRouteAttribute>
    {
        protected override string OnSetDefaultTemplateParameter() => @"/prune";

        protected override string OnSetExpectedRouteTemplate() =>
            $@"{TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate}{_defaultTemplateParameter}";
    }
}
