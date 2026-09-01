using Poly.Base;
using Poly.Math;
using Poly.Physics;

namespace Poly.Collide
{
	public interface IBroadphase
	{
		float collisionTolerance { get; set; }

		void FindPotentialPairs(ShapeHandle[] shapes, int numShapes, CollisionFilter filter, in Bounds2 bounds, ref FastList<int> potentialPairIndices, ref FastList<int> potentialPairIndices_WithTriggers, float velocityToDisplacement);
	}
}
