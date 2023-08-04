using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Server.Mvc;

namespace Rested.Core.Server.UnitTest.Mvc
{
    [TestClass]
    public class RestedSearchProjectionsRouteAttributeTest : RestedRouteAttributeTest<RestedSearchProjectionsRouteAttribute>
    {
        protected override string OnSetDefaultTemplateParameter() => @"/search";

        protected override string OnSetExpectedRouteTemplate() =>
            $@"{TestRestedRouteTemplateSettings.MultiResourceMethodRouteTemplate}{_defaultTemplateParameter}";
    }
}
