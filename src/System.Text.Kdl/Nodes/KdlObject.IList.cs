﻿namespace System.Text.Kdl.Nodes
{
    public partial class KdlObject : IList<KeyValuePair<string, KdlNode?>>
    {
        /// <summary>Gets the property the specified index.</summary>
        /// <param name="index">The zero-based index of the pair to get.</param>
        /// <returns>The property at the specified index as a key/value pair.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
        public KeyValuePair<string, KdlNode?> GetAt(int index) => Dictionary.GetAt(index);

        /// <summary>Sets a new property at the specified index.</summary>
        /// <param name="index">The zero-based index of the property to set.</param>
        /// <param name="propertyName">The property name to store at the specified index.</param>
        /// <param name="value">The KDL value to store at the specified index.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is already specified in a different index.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> already has a parent.</exception>
        public void SetAt(int index, string propertyName, KdlNode? value)
        {
            if (propertyName is null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(propertyName));
            }

            OrderedDictionary<string, KdlNode?> dictionary = Dictionary;
            KeyValuePair<string, KdlNode?> existing = dictionary.GetAt(index);
            dictionary.SetAt(index, propertyName, value);
            DetachParent(existing.Value);
            value?.AssignParent(this);
        }

        /// <summary>Sets a new property value at the specified index.</summary>
        /// <param name="index">The zero-based index of the property to set.</param>
        /// <param name="value">The KDL value to store at the specified index.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> already has a parent.</exception>
        public void SetAt(int index, KdlNode? value)
        {
            OrderedDictionary<string, KdlNode?> dictionary = Dictionary;
            KeyValuePair<string, KdlNode?> existing = dictionary.GetAt(index);
            dictionary.SetAt(index, value);
            DetachParent(existing.Value);
            value?.AssignParent(this);
        }

        /// <summary>Determines the index of a specific property name in the object.</summary>
        /// <param name="propertyName">The property name to locate.</param>
        /// <returns>The index of <paramref name="propertyName"/> if found; otherwise, -1.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        public int IndexOf(string propertyName)
        {
            if (propertyName is null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(propertyName));
            }

            return Dictionary.IndexOf(propertyName);
        }

        /// <summary>Inserts a property into the object at the specified index.</summary>
        /// <param name="index">The zero-based index at which the property should be inserted.</param>
        /// <param name="propertyName">The property name to insert.</param>
        /// <param name="value">The KDL value to insert.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the <see cref="KdlObject"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="Count"/>.</exception>
        public void Insert(int index, string propertyName, KdlNode? value)
        {
            if (propertyName is null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(propertyName));
            }

            Dictionary.Insert(index, propertyName, value);
            value?.AssignParent(this);
        }

        /// <summary>Removes the property at the specified index.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
        public void RemoveAt(int index)
        {
            KeyValuePair<string, KdlNode?> existing = Dictionary.GetAt(index);
            Dictionary.RemoveAt(index);
            DetachParent(existing.Value);
        }

        /// <inheritdoc />
        KeyValuePair<string, KdlNode?> IList<KeyValuePair<string, KdlNode?>>.this[int index]
        {
            get => GetAt(index);
            set => SetAt(index, value.Key, value.Value);
        }

        /// <inheritdoc />
        int IList<KeyValuePair<string, KdlNode?>>.IndexOf(KeyValuePair<string, KdlNode?> item) => ((IList<KeyValuePair<string, KdlNode?>>)Dictionary).IndexOf(item);

        /// <inheritdoc />
        void IList<KeyValuePair<string, KdlNode?>>.Insert(int index, KeyValuePair<string, KdlNode?> item) => Insert(index, item.Key, item.Value);

        /// <inheritdoc />
        void IList<KeyValuePair<string, KdlNode?>>.RemoveAt(int index) => RemoveAt(index);
    }
}
