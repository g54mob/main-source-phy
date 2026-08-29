using System.Collections.Generic;
using UnityEngine;

public static class RoomEditorExtensions
{
	public static List<T> GetComponentsInInstance<T>(this PropInstance instance, bool includeSelf = true)
	{
		List<T> result = new List<T>();
		collectComponentsInInstance(instance.transform, result, includeSelf);
		return result;
	}

	private static void collectComponentsInInstance<T>(Transform transform, List<T> result, bool includeSelf = true)
	{
		if (includeSelf && transform.TryGetComponent<T>(out var component))
		{
			result.Add(component);
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!(child.GetComponent<PropInstance>() != null))
			{
				collectComponentsInInstance(child, result);
			}
		}
	}

	public static int CountComponentsInInstance<T>(this PropInstance instance, bool includeSelf = true)
	{
		return countRecursive(instance.transform, includeSelf);
		static int countRecursive(Transform transform, bool flag)
		{
			int num = 0;
			if (flag && transform.TryGetComponent<T>(out var _))
			{
				num++;
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (!(child.GetComponent<PropInstance>() != null))
				{
					num += countRecursive(child, includeSelf: true);
				}
			}
			return num;
		}
	}
}
