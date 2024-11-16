using System.Text.Json;
using System.Text.Json.Serialization;
using Rested.Core.Data;
using Rested.Core.Data.Search;

namespace Rested.Core.Server.Json;

public class JsonIFilterConverter : JsonConverter<IFilter>
{
    public override IFilter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var jsonDocument))
            throw new JsonException("Failed to parse the JsonDocument!");

        if (!jsonDocument.RootElement.TryGetProperty("filterType", out var filterTypeElement))
            throw new JsonException($"The {nameof(IFilter.FilterType)} property was not found! Unable to deserialize filter!");

        if (!Enum.TryParse<FilterTypes>(filterTypeElement.GetString(), true, out var filterType))
            throw new JsonException($"The {nameof(IFilter.FilterType)} could not be parsed!");
        
        var rootElement = jsonDocument.RootElement.GetRawText();

        return filterType switch
        {
            FilterTypes.TextFieldFilter => JsonSerializer.Deserialize<TextFieldFilter>(rootElement, options)!,
            FilterTypes.NumberFieldFilter => JsonSerializer.Deserialize<NumberFieldFilter>(rootElement, options)!,
            FilterTypes.DateFieldFilter => JsonSerializer.Deserialize<DateFieldFilter>(rootElement, options)!,
            FilterTypes.DateTimeFieldFilter => JsonSerializer.Deserialize<DateTimeFieldFilter>(rootElement, options)!,
            FilterTypes.OperatorFilter => JsonSerializer.Deserialize<OperatorFilter>(rootElement, options)!,
            _ => throw new JsonException($"{filterType} could not be deserialized!")
        };
    }

    public override void Write(Utf8JsonWriter writer, IFilter value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case TextFieldFilter textFieldFilter:
                var textFieldFilterJson = JsonSerializer.Serialize(textFieldFilter, options);
                writer.WriteRawValue(textFieldFilterJson);
                break;
            
            case NumberFieldFilter numberFieldFilter:
                var numberFieldFilterJson = JsonSerializer.Serialize(numberFieldFilter, options);
                writer.WriteRawValue(numberFieldFilterJson);
                break;
            
            case DateFieldFilter dateFieldFilter:
                var dateFieldFilterJson = JsonSerializer.Serialize(dateFieldFilter, options);
                writer.WriteRawValue(dateFieldFilterJson);
                break;
            
            case DateTimeFieldFilter dateTimeFieldFilter:
                var dateTimeFieldFilterJson = JsonSerializer.Serialize(dateTimeFieldFilter, options);
                writer.WriteRawValue(dateTimeFieldFilterJson);
                break;
            
            case OperatorFilter operatorFilter:
                writer.WriteStartObject();
                writer.WriteString(nameof(IFilter.FilterType).ToCamelCase(), operatorFilter.FilterType.ToString());
                writer.WriteString(nameof(IOperatorFilter.Operator).ToCamelCase(), operatorFilter.Operator.ToString());
                writer.WriteStartArray(nameof(IOperatorFilter.Filters).ToCamelCase());
            
                operatorFilter.Filters.ForEach(filter => Write(writer, filter, options));
            
                writer.WriteEndArray();
                writer.WriteEndObject();
                break;
        }
    }
}