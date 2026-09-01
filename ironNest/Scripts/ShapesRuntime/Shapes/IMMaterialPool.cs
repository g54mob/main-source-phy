using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal static class IMMaterialPool
	{
		public static Dictionary<RenderState, Material> pool;

		static IMMaterialPool()
		{
		}

		internal static Material GetMaterial(ref RenderState state)
		{
			return null;
		}

		private static void FlushAllMaterials()
		{
		}
	}
}
