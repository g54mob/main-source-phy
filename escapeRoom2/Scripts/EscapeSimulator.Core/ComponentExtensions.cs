using UnityEngine;

public static class ComponentExtensions
{
	public static T GetOrAddComponent<T>(this Component component) where T : Component
	{
		if (!component.TryGetComponent<T>(out var component2))
		{
			return component.gameObject.AddComponent<T>();
		}
		return component2;
	}
}
