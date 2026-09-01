using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
	public static List<List<T>> Split<T>(this List<T> collection, int size)
	{
		List<List<T>> list = new List<List<T>>();
		int num = collection.Count() / size;
		if (collection.Count % size > 0)
		{
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			list.Add(collection.Skip(i * size).Take(size).ToList());
		}
		return list;
	}

	public static void AddOrReplace<K, V>(this IDictionary<K, V> dictionary, K key, V value)
	{
		if (dictionary.ContainsKey(key))
		{
			dictionary[key] = value;
		}
		else
		{
			dictionary.Add(key, value);
		}
	}

	public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key)
	{
		V value;
		return (!dictionary.TryGetValue(key, out value)) ? default(V) : value;
	}
}
