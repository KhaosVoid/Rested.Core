using System.Reflection;
using System.Text.Json.Serialization;
using Rested.Core.Data.Search;

namespace Rested.Core.Data;

public static class DataUtility
{
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
                $"{fieldName}.{property.Name}".ToCamelCase();

            validFieldNames.Add(fieldFilterFieldName);

            if (property.GetCustomAttribute<SearchIgnoreAttribute>() is not null)
                ignoredFieldNames.Add(fieldFilterFieldName);

            else if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                ignoredFieldNames.Add(fieldFilterFieldName);

            if (property.PropertyType.IsClass)
            {
                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[0], validFieldNames, ignoredFieldNames, fieldFilterFieldName);

                    else if (property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[1], validFieldNames, ignoredFieldNames, fieldFilterFieldName);
                }

                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                    GetFieldNamesFromTypeProperties(property.PropertyType, validFieldNames, ignoredFieldNames, fieldFilterFieldName);
            }

            else if (property.PropertyType.IsArray)
                GetFieldNamesFromTypeProperties(property.PropertyType.GetElementType(), validFieldNames, ignoredFieldNames, fieldFilterFieldName);
        }
    }
}