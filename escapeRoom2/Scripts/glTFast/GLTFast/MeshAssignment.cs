using UnityEngine;

namespace GLTFast
{
	internal readonly struct MeshAssignment
	{
		public readonly Mesh mesh;

		public readonly int[] primitives;

		public MeshAssignment(Mesh mesh, int[] primitives)
		{
			this.mesh = mesh;
			this.primitives = primitives;
		}
	}
}
