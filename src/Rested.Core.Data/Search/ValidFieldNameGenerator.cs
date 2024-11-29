using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Rested.Core.Data.Search;

public partial class ValidFieldNameGenerator
{
    #region Properties

    public List<string> ValidFieldNames { get; protected set; } = [];
    public List<string> IgnoredFieldNames { get; protected set; } = [];
    
    #endregion Properties
    
    #region Ctor

    public ValidFieldNameGenerator(Type inputType)
    {
        GenerateValidFieldNames(inputType);
    }

    #endregion Ctor

    #region Methods

    private void GenerateValidFieldNames(Type inputType)
    {
        var validFieldNames = new List<string>();
        var ignoredFieldNames = new List<string>();

        GetFieldNamesFromTypeProperties(inputType, validFieldNames, ignoredFieldNames);
        
        ValidFieldNames = validFieldNames;
        IgnoredFieldNames = ignoredFieldNames;
    }

    protected virtual void GetFieldNamesFromTypeProperties(Type type, List<string> validFieldNames = null, List<string> ignoredFieldNames = null, string lastFieldName = null)
    {
        var properties = type.GetProperties();

        validFieldNames ??= [];
        ignoredFieldNames ??= [];

        foreach (var property in properties)
        {
            if (property.GetCustomAttribute<SearchIgnoreAttribute>() is not null)
                ignoredFieldNames.Add($"^{CalculateFieldName(property.Name, lastFieldName)}$");
            
            else if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                ignoredFieldNames.Add($"^{CalculateFieldName(property.Name, lastFieldName)}$");

            else
                OnAddValidFieldName(property, validFieldNames, lastFieldName);

            if (property.PropertyType.IsClass)
            {
                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        OnAddValidListFieldName(property, validFieldNames, ignoredFieldNames, lastFieldName);
                    
                    else if (property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[1], validFieldNames, ignoredFieldNames, CalculateFieldName(property.Name, lastFieldName));
                }
                
                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                    GetFieldNamesFromTypeProperties(property.PropertyType, validFieldNames, ignoredFieldNames, CalculateFieldName(property.Name, lastFieldName));
            }
            
            else if (property.PropertyType.IsArray)
                OnAddValidArrayFieldName(property, validFieldNames, ignoredFieldNames, lastFieldName);
        }
    }

    protected virtual void OnAddValidFieldName(PropertyInfo property, List<string> validFieldNames, string lastFieldName = null)
    {
        var propertyName = property.Name;

        if (propertyName is "Id")
            OnAddValidIdFieldName(property, validFieldNames, lastFieldName);

        else
            validFieldNames.Add($"^{CalculateFieldName(property.Name, lastFieldName)}$");
    }

    protected virtual void OnAddValidIdFieldName(PropertyInfo property, List<string> validFieldNames, string lastFieldName = null)
    {
        validFieldNames.Add($"^{CalculateFieldName(property.Name, lastFieldName)}$");
    }
    
    protected string CalculateFieldName(string propertyName, string lastFieldName)
    {
        return string.IsNullOrWhiteSpace(lastFieldName) ?
            propertyName.ToCamelCase() :
            $@"{lastFieldName}\.{propertyName.ToCamelCase()}";
    }

    protected virtual void OnAddValidListFieldName(PropertyInfo property, List<string> validFieldNames, List<string> ignoredFieldNames, string lastFieldName = null)
    {
        var fieldName = $@"{CalculateFieldName(property.Name, lastFieldName)}\[\d*\]";
        
        validFieldNames.Add($"^{fieldName}$");
        GetFieldNamesFromTypeProperties(property.PropertyType.GenericTypeArguments[0], validFieldNames, ignoredFieldNames, fieldName);
    }

    protected virtual void OnAddValidArrayFieldName(PropertyInfo property, List<string> validFieldNames, List<string> ignoredFieldNames, string lastFieldName = null)
    {
        var fieldName = $@"{CalculateFieldName(property.Name, lastFieldName)}\[\d*\]";
        
        validFieldNames.Add($"^{fieldName}$");
        GetFieldNamesFromTypeProperties(property.PropertyType.GetElementType(), validFieldNames, ignoredFieldNames, fieldName);
    }

    public bool IsFieldNameValid(string fieldName)
    {
        if (IgnoredFieldNames.Any(ignoredFieldName => Regex.IsMatch(fieldName, ignoredFieldName)))
            return false;

        return ValidFieldNames.Any(validFieldName => Regex.IsMatch(fieldName, validFieldName));
    }

    #endregion Methods
}