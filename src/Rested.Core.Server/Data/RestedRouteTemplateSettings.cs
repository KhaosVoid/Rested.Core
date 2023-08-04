using Microsoft.Extensions.Configuration;

namespace Rested.Core.Server.Data
{
    /// <summary>
    /// Contains route template configuration used by Rested route attributes.
    /// </summary>
    public class RestedRouteTemplateSettings
    {
        #region Properties

        internal static RestedRouteTemplateSettings Instance { get; private set; }

        /// <summary>
        /// Gets or sets the ControllerRouteTemplate.<br/>
        /// Default value: <c>/api/</c>
        /// </summary>
        public string ControllerRouteTemplate { get; set; } = @"/api/";

        /// <summary>
        /// Gets or sets the SingleResourceMethodRouteTemplate.<br/>
        /// Default value: <c>[controller]</c>
        /// </summary>
        public string SingleResourceMethodRouteTemplate { get; set; } = @"[controller]";

        /// <summary>
        /// Gets or sets the SingleResourceWithIdMethodRouteTemplate.<br/>
        /// Default value: <c>[controller]/{id}</c>
        /// </summary>
        public string SingleResourceWithIdMethodRouteTemplate { get; set; } = @"[controller]/{id}";

        /// <summary>
        /// Gets or sets the MultiResourceMethodRouteTemplate.<br/>
        /// Default value: <c>[controller]s</c>
        /// </summary>
        public string MultiResourceMethodRouteTemplate { get; set; } = @"[controller]s";

        #endregion Properties

        #region Ctor

        private RestedRouteTemplateSettings()
        {

        }

        #endregion Ctor

        #region Methods

        public static void InitializeRouteTemplateSettings(IConfiguration configuration)
        {
            var routeTemplateSettings = new RestedRouteTemplateSettings();

            configuration.Bind(
                key: nameof(RestedRouteTemplateSettings),
                instance: routeTemplateSettings);

            Instance = routeTemplateSettings;
        }

        private static string CalculateRouteTemplate(string route, string template = null, bool overridesConfig = false)
        {
            if (overridesConfig)
                return template;

            if (template is not null)
                return $"{route}{template}";

            return route;
        }

        internal static string CalculateControllerRouteTemplate(string template = null, bool overridesConfig = false) =>
            CalculateRouteTemplate(Instance.ControllerRouteTemplate, template, overridesConfig);

        internal static string CalculateSingleResourceRouteTemplate(string template = null, bool overridesConfig = false) =>
            CalculateRouteTemplate(Instance.SingleResourceMethodRouteTemplate, template, overridesConfig);

        internal static string CalculateSingleResourceWithIdRouteTemplate(string template = null, bool overridesConfig = false) =>
            CalculateRouteTemplate(Instance.SingleResourceWithIdMethodRouteTemplate, template, overridesConfig);

        internal static string CalculateMultiResourceRouteTemplate(string template = null, bool overridesConfig = false) =>
            CalculateRouteTemplate(Instance.MultiResourceMethodRouteTemplate, template, overridesConfig);

        #endregion Methods
    }
}
