using UnityEngine;

public static class GameObjectExtensions
{
	public static T FetchComponent<T>(this GameObject obj) where T : Component
	{
		T val = obj.GetComponent<T>();
		if (val == null)
		{
			val = obj.AddComponent<T>();
		}
		return val;
	}

	public static T GetComponentInChildrenExclusive<T>(this GameObject obj) where T : Component
	{
		foreach (Transform item in obj.transform)
		{
			T component = item.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}
}
