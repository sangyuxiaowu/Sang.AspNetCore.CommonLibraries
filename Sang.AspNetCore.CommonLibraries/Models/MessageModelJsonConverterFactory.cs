using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sang.AspNetCore.CommonLibraries.Models
{
    internal sealed class MessageModelJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(MessageModel<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var dataType = typeToConvert.GetGenericArguments()[0];
            var converterType = typeof(MessageModelJsonConverter<>).MakeGenericType(dataType);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    internal sealed class MessageModelJsonConverter<T> : JsonConverter<MessageModel<T>>
    {
        public override MessageModel<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            var status = 0;
            if (root.TryGetProperty("status", out var statusElement) && statusElement.ValueKind == JsonValueKind.Number)
            {
                status = statusElement.GetInt32();
            }
            else if (root.TryGetProperty("code", out var codeElement) && codeElement.ValueKind == JsonValueKind.Number)
            {
                status = codeElement.GetInt32();
            }

            var msg = root.TryGetProperty("msg", out var msgElement) ? msgElement.GetString() ?? "" : "";
            T? data = default;
            if (root.TryGetProperty("data", out var dataElement) && dataElement.ValueKind != JsonValueKind.Null)
            {
                data = dataElement.Deserialize<T>(options);
            }

            string? traceId = null;
            if (root.TryGetProperty("traceId", out var traceElement) && traceElement.ValueKind != JsonValueKind.Null)
            {
                traceId = traceElement.GetString();
            }

            return new MessageModel<T> { Status = status, Msg = msg, Data = data, TraceId = traceId };
        }

        public override void Write(Utf8JsonWriter writer, MessageModel<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber(MessageModelStatusField.Name, value.Status);
            writer.WriteString("msg", value.Msg);

            if (value.Data is not null)
            {
                writer.WritePropertyName("data");
                JsonSerializer.Serialize(writer, value.Data, options);
            }

            if (!string.IsNullOrWhiteSpace(value.TraceId))
            {
                writer.WriteString("traceId", value.TraceId);
            }

            writer.WriteEndObject();
        }
    }
}