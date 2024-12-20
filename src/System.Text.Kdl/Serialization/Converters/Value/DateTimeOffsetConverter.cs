// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Text.Kdl.Schema;

namespace System.Text.Kdl.Serialization.Converters
{
    internal sealed class DateTimeOffsetConverter : KdlPrimitiveConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref KdlReader reader, Type typeToConvert, KdlSerializerOptions options)
        {
            return reader.GetDateTimeOffset();
        }

        public override void Write(KdlWriter writer, DateTimeOffset value, KdlSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        internal override DateTimeOffset ReadAsPropertyNameCore(ref KdlReader reader, Type typeToConvert, KdlSerializerOptions options)
        {
            Debug.Assert(reader.TokenType == KdlTokenType.PropertyName);
            return reader.GetDateTimeOffsetNoValidation();
        }

        internal override void WriteAsPropertyNameCore(KdlWriter writer, DateTimeOffset value, KdlSerializerOptions options, bool isWritingExtensionDataProperty)
        {
            writer.WritePropertyName(value);
        }

        internal override KdlSchema? GetSchema(KdlNumberHandling _) => new KdlSchema { Type = KdlSchemaType.String, Format = "date-time" };
    }
}