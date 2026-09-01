using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
	public static bool TryFindValue<T>(this IEnumerable<T> list, Func<T, bool> func, out T item)
	{
		item = default(T);
		return false;
	}
}
