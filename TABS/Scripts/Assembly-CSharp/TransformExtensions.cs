using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
	public static Transform FindChildRecursive(this Transform aParent, string aName)
	{
		Transform transform = aParent.Find(aName);
		if (transform != null)
		{
			return transform;
		}
		foreach (Transform item in aParent)
		{
			transform = item.FindChildRecursive(aName);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	public static Transform[] FindChildrenRecursive(this Transform aParent, string aName)
	{
		List<Transform> list = new List<Transform>();
		Transform transform = aParent.Find(aName);
		if (transform != null)
		{
			list.Add(transform);
		}
		foreach (Transform item in aParent)
		{
			transform = item.FindChildRecursive(aName);
			if (transform != null)
			{
				list.Add(transform);
			}
		}
		return list.ToArray();
	}

	public static void DestroyAllChildren(this Transform parent)
	{
		for (int num = parent.childCount - 1; num >= 0; num--)
		{
			Object.Destroy(parent.GetChild(num).gameObject);
		}
	}
}
