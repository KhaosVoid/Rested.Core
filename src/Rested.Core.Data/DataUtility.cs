using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Rested.Core.Data.Search;

namespace Rested.Core.Data;

public static partial class DataUtility
{
    [GeneratedRegex(@"\[(\d*)?\]")]
    private static partial Regex FieldNameArrayRegex();
    
    public static void GenerateValidSearchFieldNames<TResultType>(out List<string> validFieldNames, out List<string> ignoredFieldNames)
    {
        validFieldNames = [];
        ignoredFieldNames = [];

        GetFieldNamesFromTypeProperties(typeof(TResultType), validFieldNames, ignoredFieldNames);
    }

    private static void GetFieldNamesFromTypeProperties(Type type, List<string> validFieldNames = null, List<string> ignoredFieldNames = null, string fieldName = "")
    {
        var properties = type.GetProperties();

        validFieldNames ??= [];
        ignoredFieldNames ??= [];

        foreach (var property in properties)
        {
            var fieldFilterFieldName = string.IsNullOrWhiteSpace(fieldName) ?
                property.Name.ToCamelCase() :
                $@"{fieldName}\.{property.Name}".ToCamelCase();

            validFieldNames.Add($@"^{fieldFilterFieldName}$");

            if (property.GetCustomAttribute<SearchIgnoreAttribute>() is not null)
                ignoredFieldNames.Add($@"^{fieldFilterFieldName}$");

            else if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                ignoredFieldNames.Add($@"^{fieldFilterFieldName}$");

            if (property.PropertyType.IsClass)
            {
                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        validFieldNames.Add($@"^{fieldFilterFieldName}\.\d*$");
                        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[0], validFieldNames, ignoredFieldNames, $@"{fieldFilterFieldName}\.\d*");
                    }

                    //TODO: After revising the array logic, this may need to be addressed as well...
                    else if (property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[1], validFieldNames, ignoredFieldNames, fieldFilterFieldName);
                }

                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                    GetFieldNamesFromTypeProperties(property.PropertyType, validFieldNames, ignoredFieldNames, fieldFilterFieldName);
            }

            else if (property.PropertyType.IsArray)
            {
                validFieldNames.Add($@"^{fieldFilterFieldName}\.\d*$");
                GetFieldNamesFromTypeProperties(property.PropertyType.GetElementType(), validFieldNames, ignoredFieldNames, $@"{fieldFilterFieldName}\.\d*");
            }
        }
    }
    
    public static bool IsFieldNameValid(string fieldName, IEnumerable<string> validFieldNames, IEnumerable<string> ignoredFieldNames)
    {
        var fieldNameArrayMatch = FieldNameArrayRegex().Match(fieldName);

        if (fieldNameArrayMatch.Success)
        {
            fieldName = FieldNameArrayRegex().Replace(
                input: fieldName,
                replacement: $".{fieldNameArrayMatch.Groups[0].Value}");
        }

        if (ignoredFieldNames.Any(ignoredFieldName => Regex.IsMatch(fieldName, ignoredFieldName)))
            return false;

        return validFieldNames.Any(validFieldName => Regex.IsMatch(fieldName, validFieldName));
    }
}