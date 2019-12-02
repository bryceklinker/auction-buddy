using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auction.Buddy.Api.Common.Json
{
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private const string DateTimeOffsetFormat = "yyyy-MM-ddThh:mm:ss.fffZ";

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString(DateTimeOffsetFormat));
        }
    }
}