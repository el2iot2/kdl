﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Kdl.Serialization.Metadata;

namespace System.Text.Kdl.Schema
{
    /// <summary>
    /// Defines the context for the generated KDL schema for a particular node in a type graph.
    /// </summary>
    public readonly struct KdlSchemaExporterContext
    {
        internal readonly string[] _path;

        internal KdlSchemaExporterContext(
            KdlTypeInfo typeInfo,
            KdlPropertyInfo? propertyInfo,
            KdlTypeInfo? baseTypeInfo,
            string[] path)
        {
            TypeInfo = typeInfo;
            PropertyInfo = propertyInfo;
            BaseTypeInfo = baseTypeInfo;
            _path = path;
        }

        /// <summary>
        /// The <see cref="KdlTypeInfo"/> for the type being processed.
        /// </summary>
        public KdlTypeInfo TypeInfo { get; }

        /// <summary>
        /// The <see cref="KdlPropertyInfo"/> if the schema is being generated for a property.
        /// </summary>
        public KdlPropertyInfo? PropertyInfo { get; }

        /// <summary>
        /// Gets the <see cref="KdlTypeInfo"/> for polymorphic base type if the schema is being generated for a derived type.
        /// </summary>
        public KdlTypeInfo? BaseTypeInfo { get; }

        /// <summary>
        /// The path to the current node in the generated KDL schema.
        /// </summary>
        public ReadOnlySpan<string> Path => _path;
    }
}