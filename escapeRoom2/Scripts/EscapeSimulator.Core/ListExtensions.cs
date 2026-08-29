using System;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
	public static bool All<T>(this List<T> list, Func<T, bool> predicate)
	{
		bool result = true;
		foreach (T item in list)
		{
			if (!predicate(item))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public static bool Any<T>(this List<T> list, Func<T, bool> predicate)
	{
		bool result = false;
		foreach (T item in list)
		{
			if (predicate(item))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static bool IsEqualTo<T>(this List<T> list, List<T> otherList)
	{
		if (list == otherList)
		{
			return true;
		}
		if (list == null || otherList == null)
		{
			return false;
		}
		if (list.Count != otherList.Count)
		{
			return false;
		}
		bool flag = typeof(UnityEngine.Object).IsAssignableFrom(typeof(T));
		for (int i = 0; i < list.Count; i++)
		{
			if (flag)
			{
				UnityEngine.Object obj = list[i] as UnityEngine.Object;
				UnityEngine.Object obj2 = otherList[i] as UnityEngine.Object;
				if (obj != obj2)
				{
					return false;
				}
			}
			else if (!EqualityComparer<T>.Default.Equals(list[i], otherList[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static void Shuffle<T>(this List<T> list)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		for (int i = 0; i < list.Count - 1; i++)
		{
			int num = UnityEngine.Random.Range(i, list.Count);
			int index = i;
			int index2 = num;
			T val = list[num];
			T val2 = list[i];
			T val3 = (list[index] = val);
			val3 = (list[index2] = val2);
		}
		UnityEngine.Random.state = state;
	}

	public static bool TryFind<T>(this List<T> list, Predicate<T> match, out T element)
	{
		element = default(T);
		foreach (T item in list)
		{
			if (match(item))
			{
				element = item;
				return true;
			}
		}
		return false;
	}
}
