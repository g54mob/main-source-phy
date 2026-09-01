using System.Collections;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public static class CollectionExtensions
	{
		public static bool IsNull(this string text)
		{
			return false;
		}

		public static bool IsNull(this ICollection list)
		{
			return false;
		}

		public static bool IsNullOrEmpty(this ICollection list)
		{
			return false;
		}

		public static bool IsNullOrEmpty(this IEnumerable source)
		{
			return false;
		}

		public static bool HasValuesThatAreNotNull(this IEnumerable source)
		{
			return false;
		}

		public static bool IsIndexOutOfBounds(this ICollection list, int index)
		{
			return false;
		}

		public static void RemoveRange(this IList list, IEnumerable collection)
		{
		}

		public static void AddIfNotContained(this IList list, IEnumerable collection)
		{
		}

		public static void AddIfNotContained<T>(this IList<T> list, T item)
		{
		}
	}
}
