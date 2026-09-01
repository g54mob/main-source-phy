using System.Collections.Generic;

namespace Poly.Extension
{
	public static class HashSetExtension
	{
		public static void AddRange<T>(this HashSet<T> set, List<T> other)
		{
			for (int i = 0; i < other.Count; i++)
			{
				set.Add(other[i]);
			}
		}

		public static T[] ToArray<T>(this HashSet<T> set)
		{
			T[] array = new T[set.Count];
			int num = 0;
			foreach (T item in set)
			{
				array[num++] = item;
			}
			return array;
		}
	}
}
