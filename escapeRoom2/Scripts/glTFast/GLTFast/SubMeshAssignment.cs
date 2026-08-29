using GLTFast.Schema;

namespace GLTFast
{
	internal readonly struct SubMeshAssignment
	{
		public MeshPrimitiveBase Primitive { get; }

		public int VertexBufferIndex { get; }

		public SubMeshAssignment(MeshPrimitiveBase primitive, int vertexBufferIndex)
		{
			Primitive = primitive;
			VertexBufferIndex = vertexBufferIndex;
		}
	}
}
