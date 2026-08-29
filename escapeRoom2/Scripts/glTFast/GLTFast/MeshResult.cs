using UnityEngine;

namespace GLTFast
{
	public readonly struct MeshResult
	{
		public readonly int meshIndex;

		public readonly int[] primitiveIndices;

		public readonly int[] materialIndices;

		public readonly Mesh mesh;

		public MeshResult(int meshIndex, int[] primitiveIndices, int[] materialIndices, Mesh mesh)
		{
			this.meshIndex = meshIndex;
			this.primitiveIndices = primitiveIndices;
			this.materialIndices = materialIndices;
			this.mesh = mesh;
		}
	}
}
