using Poly.Base;
using Poly.Collide;
using Poly.Solver;

namespace Poly.Physics
{
	public struct WorldCollisionInput
	{
		public ShapeHandle[] shapeHandles;

		public FastList<int> broadphasePairs;

		public FastList<CollisionCache> caches;

		public SolverNode[] nodesPtr;

		public Motion[] solverMotionsPtr;
	}
}
