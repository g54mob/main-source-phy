using UnityEngine;

namespace Shapes
{
	internal static class ShapesMeshUtils
	{
		private static Mesh quadMesh;

		private static Mesh triangleMesh;

		private static Mesh sphereMesh;

		private static Mesh cuboidMesh;

		private static Mesh torusMesh;

		private static Mesh coneMesh;

		private static Mesh coneMeshUncapped;

		private static Mesh cylinderMesh;

		private static Mesh capsuleMesh;

		public static Mesh[] QuadMesh => null;

		public static Mesh[] TriangleMesh => null;

		public static Mesh[] SphereMesh => null;

		public static Mesh[] CuboidMesh => null;

		public static Mesh[] TorusMesh => null;

		public static Mesh[] ConeMesh => null;

		public static Mesh[] ConeMeshUncapped => null;

		public static Mesh[] CylinderMesh => null;

		public static Mesh[] CapsuleMesh => null;

		private static Mesh EnsureValidMeshBounds(Mesh mesh, Bounds bounds)
		{
			return null;
		}

		public static Mesh GetLineMesh(LineGeometry geometry, LineEndCap endCaps, DetailLevel detail)
		{
			return null;
		}
	}
}
