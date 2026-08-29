using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	public static class FixedList512BytesExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool MC2IsCapacity<T>(this ref FixedList512Bytes<T> fixedList) where T : unmanaged, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2Set<T>(this ref FixedList512Bytes<T> fixedList, T item) where T : unmanaged, IEquatable<T>
		{
			if (!Unity.Collections.FixedList512BytesExtensions.Contains(ref fixedList, item))
			{
				fixedList.Add(in item);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2SetLimit<T>(this ref FixedList512Bytes<T> fixedList, T item) where T : unmanaged, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning($"FixedSet512.Limit!:{fixedList.Capacity}");
			}
			else if (!Unity.Collections.FixedList512BytesExtensions.Contains(ref fixedList, item))
			{
				fixedList.Add(in item);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2RemoveItemAtSwapBack<T>(this ref FixedList512Bytes<T> fixedList, T item) where T : unmanaged, IEquatable<T>
		{
			for (int i = 0; i < fixedList.Length; i++)
			{
				if (fixedList.ElementAt(i).Equals(item))
				{
					fixedList.RemoveAtSwapBack(i);
					break;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2Push<T>(this ref FixedList512Bytes<T> fixedList, T item) where T : unmanaged, IEquatable<T>
		{
			fixedList.Add(in item);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T MC2Pop<T>(this ref FixedList512Bytes<T> fixedList) where T : unmanaged, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2Enqueue<T>(this ref FixedList512Bytes<T> fixedList, T item) where T : unmanaged, IEquatable<T>
		{
			fixedList.Add(in item);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T MC2Dequque<T>(this ref FixedList512Bytes<T> fixedList) where T : unmanaged, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
