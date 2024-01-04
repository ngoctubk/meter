using System.Text.Json;
using System.Text.Json.Serialization;

namespace SyncJob.Helpers
{
    public class EmptyStringToDoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString();

                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    // Return a default value or handle the empty string case as needed
                    return 0.0;
                }

                // If the string is not empty, attempt to parse it as a double
                if (double.TryParse(stringValue, out double result))
                {
                    return result;
                }
                else
                {
                    // Handle the case where the string cannot be parsed as a double
                    throw new JsonException($"Unable to parse '{stringValue}' as a double.");
                }
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                // Handle the case where the value is null, if necessary
                return 0.0;
            }

            // If the token is not a string or null, use the default deserialization
            return JsonSerializer.Deserialize<double>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            // Implement custom serialization logic if needed
            writer.WriteNumberValue(value);
        }
    }
}
