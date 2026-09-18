using System.Collections.Generic;
using UnityEngine;

public static class ListSystem
{
	public static void MoveToEnd<T>(this List<T> list, T item)
	{
		list.Move(item, list.Count - 1);
	}

	public static void Move<T>(this List<T> list, T item, int newIndex)
	{
		list.Remove(item);
		list.Insert(newIndex, item);
	}

	public static int GetIndexBasedOnPercent<T>(List<T> list, float percent)
	{
		if (list.Count == 0)
		{
			Debug.LogError("List is Empty");
			return 0;
		}
		if (percent == 0f)
		{
			return 0;
		}
		if (percent == 1f)
		{
			return list.Count - 1;
		}
		return Mathf.Clamp(Mathf.FloorToInt(percent * (float)(list.Count - 1)), 0, list.Count - 1);
	}
}
