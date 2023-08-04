using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rested.Core.Server.Data;
using System.Reflection;

namespace Rested.Core.Server.UnitTest.Mvc
{
    public abstract class RestedRouteAttributeTest<TRouteAttribute> where TRouteAttribute : RouteAttribute
    {
        #region Properties

        public TestContext TestContext { get; set; }
        protected RestedRouteTemplateSettings TestRestedRouteTemplateSettings { get; private set; }

        #endregion Properties

        #region Members

        protected string _defaultTemplateParameter;
        protected string _expectedRouteTemplate;

        protected readonly string TESTCONTEXTMSG_TEST_STATUS = "Test {0}: {1}";

        #endregion Members

        #region Initialization

        [TestInitialize]
        public void Initialize()
        {
            TestContext.WriteLine(
                format: TESTCONTEXTMSG_TEST_STATUS,
                args: new[] { TestContext.TestName, TestContext.CurrentTestOutcome.ToString() });
            TestContext.WriteLine(string.Empty);

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
            TestContext.WriteLine("Initializing Test RestedRouteTemplateSettings...");
            OnInitializeTestRestedRouteTemplateSettings();

            _defaultTemplateParameter = OnSetDefaultTemplateParameter();
            _expectedRouteTemplate = OnSetExpectedRouteTemplate();
        }

        protected virtual void OnInitializeTestRestedRouteTemplateSettings()
        {
            RestedRouteTemplateSettings.InitializeRouteTemplateSettings(Substitute.For<IConfiguration>());

            var restedRouteTemplateSettingsInstance = typeof(RestedRouteTemplateSettings)
                .GetProperty("Instance", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            TestRestedRouteTemplateSettings = (RestedRouteTemplateSettings)restedRouteTemplateSettingsInstance;
        }

        protected virtual string OnSetDefaultTemplateParameter()
        {
            return null;
        }

        protected abstract string OnSetExpectedRouteTemplate();

        #endregion Initialization

        #region Test Cleanup

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine(string.Empty);
            TestContext.WriteLine(
                format: TESTCONTEXTMSG_TEST_STATUS,
                args: new[] { TestContext.TestName, TestContext.CurrentTestOutcome.ToString() });
        }

        #endregion Test Cleanup

        #region Methods

        protected virtual TRouteAttribute CreateRouteAttribute() =>
            Activator.CreateInstance<TRouteAttribute>();

        protected virtual TRouteAttribute CreateRouteAttribute(string template, bool overridesConfig = false)
        {
            return (TRouteAttribute)Activator.CreateInstance(
                type: typeof(TRouteAttribute),
                args: new object[] { template, overridesConfig });
        }

        protected void TestRouteTemplate()
        {
            var routeAttribute = CreateRouteAttribute();

            routeAttribute.Template.Should().Be(_expectedRouteTemplate);
        }

        protected void TestRouteTemplate(string routeTemplate, bool routeOverridesConfig = false)
        {
            var routeAttribute = CreateRouteAttribute(routeTemplate, routeOverridesConfig);

            routeAttribute.Template.Should().Be(_expectedRouteTemplate);
        }

        #endregion Methods

        #region Route Attribute Tests

        [TestMethod]
        public void CanCreateWithDefaultParameters()
        {
            TestRouteTemplate();
        }

        [TestMethod]
        public void CanCreateWithAddedTemplate()
        {
            var defaultTemplateLastIndex = _expectedRouteTemplate.LastIndexOf(_defaultTemplateParameter ??= string.Empty);

            _expectedRouteTemplate = _expectedRouteTemplate.Remove(defaultTemplateLastIndex) + "/test";

            TestRouteTemplate(
                routeTemplate: "/test");
        }

        [TestMethod]
        public void CanCreateWithOverriddenTemplate()
        {
            _expectedRouteTemplate = "/test";

            TestRouteTemplate(
                routeTemplate: "/test",
                routeOverridesConfig: true);
        }

        #endregion Route Attribute Tests
    }
}
