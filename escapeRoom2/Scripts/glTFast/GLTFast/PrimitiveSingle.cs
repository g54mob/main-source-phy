using GLTFast.Schema;

namespace GLTFast
{
	internal class PrimitiveSingle : IPrimitiveSet
	{
		private readonly int m_Index;

		public MeshPrimitiveBase Primitive { get; }

		public bool HasMorphTargets
		{
			get
			{
				if (Primitive.targets != null)
				{
					return Primitive.targets.Length != 0;
				}
				return false;
			}
		}

		public PrimitiveSingle(int index, MeshPrimitiveBase primitive)
		{
			m_Index = index;
			Primitive = primitive;
		}

		public void BuildAndDispose(out int[] indices, out SubMeshAssignment[] subMeshAssignments)
		{
			indices = new int[1] { m_Index };
			subMeshAssignments = null;
		}

		public void BuildAndDispose(out int[] indices, out MeshPrimitiveBase[] primitives, out SubMeshAssignment[] subMeshAssignments)
		{
			indices = new int[1] { m_Index };
			primitives = new MeshPrimitiveBase[1] { Primitive };
			subMeshAssignments = null;
		}
	}
}
