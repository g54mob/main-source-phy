using UnityEngine;

namespace LevelCreator
{
	public static class DMComponentExtensions
	{
		public static bool TryGetComponentInChildren<T>(this Component c, out T result) where T : Component
		{
			result = c.GetComponentInChildren<T>();
			return result != null;
		}
	}
}
