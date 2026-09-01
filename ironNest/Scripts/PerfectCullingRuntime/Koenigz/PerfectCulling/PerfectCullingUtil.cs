using UnityEngine;
using UnityEngine.Rendering;

namespace Koenigz.PerfectCulling
{
	public static class PerfectCullingUtil
	{
		public enum ObjectByTypeSortMode
		{
			InstanceSort = 0,
			Unsorted = 1
		}

		public static string FormatNumber(int number)
		{
			return null;
		}

		public static void ToggleRenderer(Renderer r, bool visible, ShadowCastingMode defaultShadowCastingMode)
		{
		}

		public static T[] GetObjectsByType<T>(ObjectByTypeSortMode sortMode = ObjectByTypeSortMode.InstanceSort)
		{
			return null;
		}
	}
}
