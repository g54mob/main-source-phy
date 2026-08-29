using System;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions
{
	public static bool All<T>(this T[] array, Func<T, bool> predicate)
	{
		bool result = true;
		foreach (T arg in array)
		{
			if (!predicate(arg))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public static bool Any<T>(this T[] array, Func<T, bool> predicate)
	{
		bool result = false;
		foreach (T arg in array)
		{
			if (predicate(arg))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static bool IsEqualTo<T>(this T[] array, T[] otherArray)
	{
		if (array == otherArray)
		{
			return true;
		}
		if (array == null || otherArray == null)
		{
			return false;
		}
		if (array.Length != otherArray.Length)
		{
			return false;
		}
		bool flag = typeof(UnityEngine.Object).IsAssignableFrom(typeof(T));
		for (int i = 0; i < array.Length; i++)
		{
			if (flag)
			{
				UnityEngine.Object obj = array[i] as UnityEngine.Object;
				UnityEngine.Object obj2 = otherArray[i] as UnityEngine.Object;
				if (obj != obj2)
				{
					return false;
				}
			}
			else if (!EqualityComparer<T>.Default.Equals(array[i], otherArray[i]))
			{
				return false;
			}
		}
		return true;
	}
}
