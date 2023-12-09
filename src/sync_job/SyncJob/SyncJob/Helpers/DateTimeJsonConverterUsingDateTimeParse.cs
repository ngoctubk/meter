using System.Text.Json.Serialization;
using System.Text.Json;

namespace SyncJob.Helpers
{
    public class DateTimeJsonConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var d = DateTimeOffset.Parse(reader.GetString());
            return DateTimeOffset.Parse(reader.GetString()).DateTime.ToUniversalTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
