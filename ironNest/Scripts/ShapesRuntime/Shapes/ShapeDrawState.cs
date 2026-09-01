using UnityEngine;

namespace Shapes
{
	internal struct ShapeDrawState
	{
		public Mesh mesh;

		public Material mat;

		public int submesh;

		internal bool CompatibleWith(ShapeDrawState other)
		{
			return false;
		}
	}
}
