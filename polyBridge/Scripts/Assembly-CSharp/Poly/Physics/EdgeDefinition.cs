using System;

namespace Poly.Physics
{
	[Serializable]
	public class EdgeDefinition
	{
		public float lengthOverride = -1f;

		public EdgeMaterial material;

		public float stiffness = 1f;

		public float damping = 1f;

		public bool forceDamping;

		public void InitDefaults()
		{
			lengthOverride = -1f;
			stiffness = 1f;
			damping = 1f;
			forceDamping = false;
		}
	}
}
