using UnityEngine;

public static class GameObjectExtensions
{
	public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component) where T : Component
	{
		component = gameObject.GetComponentInParent<T>();
		return component != null;
	}

	public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component) where T : Component
	{
		component = gameObject.GetComponentInChildren<T>();
		return component != null;
	}

	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		if (!gameObject.TryGetComponent<T>(out var component))
		{
			return gameObject.AddComponent<T>();
		}
		return component;
	}
}
