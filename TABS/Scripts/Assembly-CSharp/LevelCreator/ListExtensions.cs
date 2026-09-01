using System.Collections.Generic;

namespace LevelCreator
{
	public static class ListExtensions
	{
		public static void Shift<T>(this List<T> list, bool down)
		{
			if (down)
			{
				T item = list[0];
				list.RemoveAt(0);
				list.Add(item);
			}
			else
			{
				T item2 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				list.Insert(0, item2);
			}
		}
	}
}
