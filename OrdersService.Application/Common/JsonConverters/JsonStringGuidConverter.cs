using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.JsonConverters
{
    public class JsonStringGuidConverter : JsonConverter<Guid>
    {
        public override void Write(Utf8JsonWriter writer, Guid id, JsonSerializerOptions options)
            => writer.WriteStringValue(id.ToString());

        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var id = reader.GetString();
            Guid.TryParse(id, out var result);
            return result;
        }
    }
}
