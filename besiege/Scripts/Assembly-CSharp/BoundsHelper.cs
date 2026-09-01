using UnityEngine;

public static class BoundsHelper
{
	public static Bounds GetContentBounds(Transform transform)
	{
		Bounds result = new Bounds(transform.position, Vector3.zero);
		Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			result.Encapsulate(renderer.bounds);
		}
		return result;
	}
}
