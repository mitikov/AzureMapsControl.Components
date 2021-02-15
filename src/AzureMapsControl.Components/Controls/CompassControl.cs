﻿namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the rotation of the map.
    /// </summary>
    [JsonConverter(typeof(CompassControlJsonConverter))]
    public sealed class CompassControl : Control<CompassControlOptions>
    {
        internal override string Type => "compass";

        /// <summary>
        /// Position of the control
        /// </summary>
        public ControlPosition Position { get; set; }

        public CompassControl(CompassControlOptions options = null, ControlPosition position = null) : base(options) => Position = position;
    }

    internal class CompassControlJsonConverter : JsonConverter<CompassControl>
    {
        public override CompassControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, CompassControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, CompassControl value)
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
                if (value.Options.RotationDegreesDelta.HasValue)
                {
                    writer.WriteNumber("rotationDegreesDelta", value.Options.RotationDegreesDelta.Value);
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
