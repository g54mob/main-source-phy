using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	public static class ShapesMeshPool
	{
		private static int meshesAllocated;

		private static Stack<Mesh> meshPool;

		public static int MeshCountInPool => 0;

		public static int MeshesAllocatedCount => 0;

		public static int MeshCountInUse => 0;

		public static Mesh GetMesh()
		{
			return null;
		}

		public static void Release(Mesh m)
		{
		}
	}
}
