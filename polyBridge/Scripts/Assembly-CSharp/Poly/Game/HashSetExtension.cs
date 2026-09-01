using System.Collections.Generic;

namespace Poly.Game
{
	public static class HashSetExtension
	{
		public static void AddTwo<T>(this HashSet<T> hashSet, in T one, in T two)
		{
			hashSet.Add(one);
			hashSet.Add(two);
		}

		public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> listToAdd)
		{
			foreach (T item in listToAdd)
			{
				hashSet.Add(item);
			}
		}

		public static void RemoveRange<T>(this HashSet<T> hashSet, List<T> listToRemove)
		{
			listToRemove.ForEach(delegate(T e)
			{
				hashSet.Remove(e);
			});
		}
	}
}
