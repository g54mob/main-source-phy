using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RenderMeshesBatch
	{
		public readonly Mesh Mesh;

		public readonly Material Material;

		protected Matrix4x4[] m_matrices;

		public Matrix4x4[] Matrices => m_matrices;

		public RenderMeshesBatch(Mesh mesh, Material material, Matrix4x4[] matrices)
		{
			Mesh = mesh;
			Material = material;
			m_matrices = matrices;
		}

		public virtual void Refresh()
		{
		}
	}
}
