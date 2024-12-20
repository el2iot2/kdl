﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Text.Kdl.Serialization
{
    /// <summary>
    /// Defines how deserializing a type declared as an <see cref="object"/> is handled during deserialization.
    /// </summary>
    public enum KdlUnknownTypeHandling
    {
        /// <summary>
        /// A type declared as <see cref="object"/> is deserialized as a <see cref="KdlElement"/>.
        /// </summary>
        KdlElement = 0,
        /// <summary>
        /// A type declared as <see cref="object"/> is deserialized as a <see cref="KdlNode"/>.
        /// </summary>
        KdlNode = 1
    }
}