using System;

namespace Poly.Extension
{
	public static class ArrayExtension
	{
		public static T GetLast<T>(this T[] array)
		{
			return array[^1];
		}

		public static T GetAtMod<T>(this T[] array, int idx)
		{
			return array[idx % array.Length];
		}

		public static void NullRefs<T>(this T[] array) where T : class
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = null;
			}
		}

		public static void ForEach<T>(this T[] array, Action<T> a)
		{
			foreach (T obj in array)
			{
				a(obj);
			}
		}
	}
}
