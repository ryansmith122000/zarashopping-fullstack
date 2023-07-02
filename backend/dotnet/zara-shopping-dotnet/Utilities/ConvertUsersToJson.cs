using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZaraShopping.Utilities
{
    public static class JsonSerializationUtility
    {
        public static string ConvertToJson<T>(T obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IgnoreNullValues = true, // Exclude null values from the JSON
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Use camel case for property names
            };

            // Exclude the address.id property
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new ExcludePropertyConverter<T>("Id"));

            return JsonSerializer.Serialize(obj, options);
        }
    }

    // Custom converter to exclude a specific property from serialization
    public class ExcludePropertyConverter<T> : JsonConverter<T>
    {
        private readonly string propertyName;

        public ExcludePropertyConverter(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property != null)
            {
                writer.WriteStartObject();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop != property)
                    {
                        writer.WritePropertyName(prop.Name);
                        JsonSerializer.Serialize(writer, prop.GetValue(value), options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
}
