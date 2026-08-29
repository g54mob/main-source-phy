using System.Collections.Generic;
using GLTFast.Schema;

namespace GLTFast
{
	internal class PrimitiveSet : IPrimitiveSet
	{
		private readonly List<int> m_Indices = new List<int>();

		private readonly List<MeshPrimitiveBase> m_Primitives = new List<MeshPrimitiveBase>();

		private List<SubMeshAssignment> m_SubMeshAssignments;

		public IReadOnlyList<MeshPrimitiveBase> Primitives => m_Primitives;

		public bool HasMorphTargets
		{
			get
			{
				if (m_Primitives[0].targets != null)
				{
					return m_Primitives[0].targets.Length != 0;
				}
				return false;
			}
		}

		public void Add(int index, MeshPrimitiveBase primitive)
		{
			if (m_Primitives.Count > 0)
			{
				for (int i = 0; i < m_Primitives.Count; i++)
				{
					if (!PrimitiveComparer.HaveEqualVertexBuffers(m_Primitives[i], primitive))
					{
						continue;
					}
					if (m_SubMeshAssignments == null)
					{
						m_SubMeshAssignments = new List<SubMeshAssignment>(m_Indices.Count + 1);
						for (int j = 0; j < m_Indices.Count; j++)
						{
							m_SubMeshAssignments.Add(new SubMeshAssignment(m_Primitives[j], j));
						}
					}
					m_Indices.Add(index);
					m_SubMeshAssignments.Add(new SubMeshAssignment(primitive, i));
					return;
				}
			}
			m_SubMeshAssignments?.Add(new SubMeshAssignment(primitive, m_Primitives.Count));
			m_Indices.Add(index);
			m_Primitives.Add(primitive);
		}

		public void BuildAndDispose(out int[] indices, out SubMeshAssignment[] subMeshAssignments)
		{
			indices = m_Indices.ToArray();
			subMeshAssignments = m_SubMeshAssignments?.ToArray();
			m_Indices.Clear();
			m_Primitives.Clear();
			m_SubMeshAssignments?.Clear();
			m_SubMeshAssignments = null;
		}

		public void BuildAndDispose(out int[] indices, out MeshPrimitiveBase[] primitives, out SubMeshAssignment[] subMeshAssignments)
		{
			indices = m_Indices.ToArray();
			primitives = m_Primitives.ToArray();
			subMeshAssignments = m_SubMeshAssignments?.ToArray();
			m_Indices.Clear();
			m_Primitives.Clear();
			m_SubMeshAssignments?.Clear();
			m_SubMeshAssignments = null;
		}
	}
}
