﻿using System.Diagnostics;
using System.Text.Kdl.Nodes;
using System.Text.Kdl.Schema;

namespace System.Text.Kdl.Serialization.Converters
{
    /// <summary>
    /// Converter for KdlNode-derived types. The {T} value must be Object and not KdlNode
    /// since we allow Object-declared members\variables to deserialize as {KdlNode}.
    /// </summary>
    internal sealed class KdlNodeConverter : KdlConverter<KdlNode?>
    {
        private static KdlNodeConverter? s_nodeConverter;
        private static KdlArrayConverter? s_arrayConverter;
        private static KdlObjectConverter? s_objectConverter;
        private static KdlValueConverter? s_valueConverter;

        public static KdlNodeConverter Instance => s_nodeConverter ??= new KdlNodeConverter();
        public static KdlArrayConverter ArrayConverter => s_arrayConverter ??= new KdlArrayConverter();
        public static KdlObjectConverter ObjectConverter => s_objectConverter ??= new KdlObjectConverter();
        public static KdlValueConverter ValueConverter => s_valueConverter ??= new KdlValueConverter();

        public override void Write(KdlWriter writer, KdlNode? value, KdlSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                value.WriteTo(writer, options);
            }
        }

        public override KdlNode? Read(ref KdlReader reader, Type typeToConvert, KdlSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case KdlTokenType.String:
                case KdlTokenType.False:
                case KdlTokenType.True:
                case KdlTokenType.Number:
                    return ValueConverter.Read(ref reader, typeToConvert, options);
                case KdlTokenType.StartObject:
                    return ObjectConverter.Read(ref reader, typeToConvert, options);
                case KdlTokenType.StartArray:
                    return ArrayConverter.Read(ref reader, typeToConvert, options);
                case KdlTokenType.Null:
                    return null;
                default:
                    Debug.Assert(false);
                    throw new KdlException();
            }
        }

        public static KdlNode? Create(KdlElement element, KdlNodeOptions? options)
        {
            KdlNode? node = element.ValueKind switch
            {
                KdlValueKind.Null => null,
                KdlValueKind.Object => new KdlObject(element, options),
                KdlValueKind.Array => new KdlArray(element, options),
                _ => new KdlValueOfElement(element, options),
            };
            return node;
        }

        internal override KdlSchema? GetSchema(KdlNumberHandling _) => KdlSchema.CreateTrueSchema();
    }
}
