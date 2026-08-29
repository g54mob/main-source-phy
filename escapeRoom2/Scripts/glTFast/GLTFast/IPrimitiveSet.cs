using GLTFast.Schema;

namespace GLTFast
{
	internal interface IPrimitiveSet
	{
		bool HasMorphTargets { get; }

		void BuildAndDispose(out int[] indices, out MeshPrimitiveBase[] primitives, out SubMeshAssignment[] subMeshAssignments);
	}
}
