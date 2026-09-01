using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Poly.Extension
{
	public static class ListExtension
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetLast<T>(this List<T> list)
		{
			return list[list.Count - 1];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveLast<T>(this List<T> list)
		{
			list.RemoveAt(list.Count - 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T PopLast<T>(this List<T> list)
		{
			int index = list.Count - 1;
			T result = list[index];
			list.RemoveAt(index);
			return result;
		}

		public static T RemoveAtAndSwap<T>(this List<T> list, int removeIndex)
		{
			int index = list.Count - 1;
			T result = (list[removeIndex] = list[index]);
			list.RemoveAt(index);
			return result;
		}
	}
}
