// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Kdl.Serialization.Metadata;

namespace System.Text.Kdl.Serialization.Converters
{
    // Converter for F# sets: https://fsharp.github.io/fsharp-core-docs/reference/fsharp-collections-fsharpset-1.html
    internal sealed class FSharpSetConverter<TSet, TElement> : IEnumerableDefaultConverter<TSet, TElement>
        where TSet : IEnumerable<TElement>
    {
        private readonly Func<IEnumerable<TElement>, TSet> _setConstructor;

        [RequiresUnreferencedCode(FSharpCoreReflectionProxy.FSharpCoreUnreferencedCodeMessage)]
        [RequiresDynamicCode(FSharpCoreReflectionProxy.FSharpCoreUnreferencedCodeMessage)]
        public FSharpSetConverter()
        {
            _setConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpSetConstructor<TSet, TElement>();
        }

        protected override void Add(in TElement value, ref ReadStack state)
        {
            ((List<TElement>)state.Current.ReturnValue!).Add(value);
        }

        internal override bool SupportsCreateObjectDelegate => false;
        protected override void CreateCollection(ref KdlReader reader, scoped ref ReadStack state, KdlSerializerOptions options)
        {
            state.Current.ReturnValue = new List<TElement>();
        }

        internal sealed override bool IsConvertibleCollection => true;
        protected override void ConvertCollection(ref ReadStack state, KdlSerializerOptions options)
        {
            state.Current.ReturnValue = _setConstructor((List<TElement>)state.Current.ReturnValue!);
        }
    }
}