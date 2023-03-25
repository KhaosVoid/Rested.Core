using System.Text.Json;

namespace Rested.Core.CQRS.Data
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a string to camelcase. Supports strings using dot notation (e.g. <c>object.myVariable</c>)
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            var sections = value.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sections?.Length; i++)
                sections[i] = JsonNamingPolicy.CamelCase.ConvertName(sections[i]);

            return string.Join(".", sections);
        }
    }
}
