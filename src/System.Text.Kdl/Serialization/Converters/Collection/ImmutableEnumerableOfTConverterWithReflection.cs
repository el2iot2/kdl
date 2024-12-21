using System.Diagnostics.CodeAnalysis;
using System.Text.Kdl.Serialization.Metadata;

namespace System.Text.Kdl.Serialization.Converters
{
    [method: RequiresUnreferencedCode(IEnumerableConverterFactoryHelpers.ImmutableConvertersUnreferencedCodeMessage)]
    [method: RequiresDynamicCode(IEnumerableConverterFactoryHelpers.ImmutableConvertersUnreferencedCodeMessage)]
    internal sealed class ImmutableEnumerableOfTConverterWithReflection<TCollection, TElement>()
        : ImmutableEnumerableOfTConverter<TCollection, TElement>
        where TCollection : IEnumerable<TElement>
    {
        [RequiresUnreferencedCode(IEnumerableConverterFactoryHelpers.ImmutableConvertersUnreferencedCodeMessage)]
        [RequiresDynamicCode(IEnumerableConverterFactoryHelpers.ImmutableConvertersUnreferencedCodeMessage)]
        internal override void ConfigureKdlTypeInfoUsingReflection(KdlTypeInfo jsonTypeInfo, KdlSerializerOptions options)
        {
            jsonTypeInfo.CreateObjectWithArgs = DefaultKdlTypeInfoResolver.MemberAccessor.CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>();
        }
    }
}
