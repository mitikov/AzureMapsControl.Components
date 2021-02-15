﻿namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the zoom of the map.
    /// </summary>
    [JsonConverter(typeof(ZoomControlJsonConverter))]
    public sealed class ZoomControl : Control<ZoomControlOptions>
    {
        internal override string Type => "zoom";

        public ControlPosition Position { get; set; }

        public ZoomControl(ZoomControlOptions options = null, ControlPosition position = null) : base(options) => Position = position;
    }

    internal sealed class ZoomControlJsonConverter : JsonConverter<ZoomControl>
    {
        public override ZoomControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, ZoomControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, ZoomControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.Type);
            if (value.Position is not null)
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Options is not null)
            {
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.ZoomDelta.HasValue)
                {
                    writer.WriteNumber("zoomDelta", value.Options.ZoomDelta.Value);
                }
                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
