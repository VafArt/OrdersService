﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrdersService.Application.Common.JsonConverters
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string Format;
        public CustomDateTimeConverter()
        {
            Format = "yyyy-MM-dd HH:mm.ss";
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
            => writer.WriteStringValue(date.ToString(Format));

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }
}
