using UnityEngine;

public static class LayerMaskExtensions
{
	public static bool Contains(this LayerMask mask, int layer)
	{
		return (int)mask == ((int)mask | (1 << layer));
	}
}
