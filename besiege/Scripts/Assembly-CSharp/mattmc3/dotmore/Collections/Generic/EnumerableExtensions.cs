using System;
using System.Collections.Generic;
using System.Linq;
using Ordered;

namespace mattmc3.dotmore.Collections.Generic
{
	public static class EnumerableExtensions
	{
		public static T Coalesce<T>(this IEnumerable<T> e)
		{
			if (e == null)
			{
				return default(T);
			}
			return e.Where((T x) => x != null).FirstOrDefault();
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> e, T firstElement)
		{
			return new T[1] { firstElement }.Concat(e);
		}

		public static IEnumerable<T> Append<T>(this IEnumerable<T> e, T lastElement)
		{
			return e.Concat(new T[1] { lastElement });
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> e)
		{
			List<T> list = e.ToList();
			Random random = new Random();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				int r = random.Next(i + 1);
				yield return list[r];
				list[r] = list[i];
			}
		}

		public static IEnumerable<T> Cycle<T>(this IEnumerable<T> e, int? numberOfCycles = null)
		{
			if (e == null)
			{
				yield break;
			}
			bool isEmpty = false;
			int count = 0;
			while (!isEmpty && (!numberOfCycles.HasValue || (numberOfCycles.HasValue && count < numberOfCycles.Value)))
			{
				isEmpty = true;
				foreach (T item in e)
				{
					yield return item;
					isEmpty = false;
				}
				count++;
			}
		}

		public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			using (IEnumerator<T> iterator = source.GetEnumerator())
			{
				if (!iterator.MoveNext())
				{
					yield break;
				}
				List<T> list = new List<T> { iterator.Current };
				while (iterator.MoveNext())
				{
					if (predicate(iterator.Current))
					{
						list.Add(iterator.Current);
						continue;
					}
					yield return list;
					list = new List<T> { iterator.Current };
				}
				yield return list;
			}
		}

		public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize = 10)
		{
			return new PagedList<T>(source, index, pageSize);
		}

		public static Ordered.Dictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Func<TSource, TSource> elementSelector = (TSource x) => x;
			return GetOrderedDictionaryImpl(source, keySelector, elementSelector, null);
		}

		public static Ordered.Dictionary<TKey, TElement> ToOrderedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return GetOrderedDictionaryImpl(source, keySelector, elementSelector, null);
		}

		public static Ordered.Dictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Func<TSource, TSource> elementSelector = (TSource x) => x;
			return GetOrderedDictionaryImpl(source, keySelector, elementSelector, comparer);
		}

		public static Ordered.Dictionary<TKey, TElement> ToOrderedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return GetOrderedDictionaryImpl(source, keySelector, elementSelector, comparer);
		}

		private static Ordered.Dictionary<TKey, TElement> GetOrderedDictionaryImpl<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Ordered.Dictionary<TKey, TElement> dictionary = null;
			dictionary = ((comparer != null) ? new Ordered.Dictionary<TKey, TElement>(comparer) : new Ordered.Dictionary<TKey, TElement>());
			foreach (TSource item in source)
			{
				dictionary.Add(keySelector(item), elementSelector(item));
			}
			return dictionary;
		}
	}
}
