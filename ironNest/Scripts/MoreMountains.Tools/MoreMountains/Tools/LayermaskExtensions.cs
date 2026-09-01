using UnityEngine;

namespace MoreMountains.Tools
{
	public static class LayermaskExtensions
	{
		public static bool MMContains(this LayerMask mask, int layer)
		{
			return false;
		}

		public static bool MMContains(this LayerMask mask, GameObject gameobject)
		{
			return false;
		}
	}
}
