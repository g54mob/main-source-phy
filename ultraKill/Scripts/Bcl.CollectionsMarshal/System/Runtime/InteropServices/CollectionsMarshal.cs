using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	public static class CollectionsMarshal
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T?> AsSpan<T>(List<T>? list)
		{
			if (list != null)
			{
				return new Span<T>(list._items, 0, list._size);
			}
			return default(Span<T>);
		}

		public unsafe static ref TValue? GetValueRefOrNullRef<TKey, TValue>(Dictionary<TKey, TValue?> dictionary, TKey key) where TKey : notnull
		{
			int num = dictionary.FindEntry(key);
			if (num >= 0)
			{
				return ref dictionary._entries[num].value;
			}
			return ref *(TValue?*)null;
		}

		public static ref TValue? GetValueRefOrAddDefault<TKey, TValue>(Dictionary<TKey, TValue?> dictionary, TKey key, out bool exists) where TKey : notnull
		{
			int num = dictionary.FindEntry(key);
			exists = true;
			if (num < 0)
			{
				exists = false;
				dictionary.Add(key, default(TValue));
				num = dictionary.FindEntry(key);
			}
			return ref dictionary._entries[num].value;
		}

		public static void SetCount<T>(List<T> list, int count)
		{
			if (count < 0)
			{
				ThrowArgumentOutOfRangeException_NeedNonNegNum("count");
			}
			list._version++;
			if (count > list.Capacity)
			{
				list.EnsureCapacity(count);
			}
			else if (count < list._size && RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(list._items, count, list._size - count);
			}
			list._size = count;
		}

		private static void ThrowArgumentOutOfRangeException_NeedNonNegNum(string paramName)
		{
			throw new ArgumentOutOfRangeException(paramName, "Non-negative number required.");
		}
	}
}
