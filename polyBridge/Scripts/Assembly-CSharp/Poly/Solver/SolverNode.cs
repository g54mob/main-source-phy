using System;
using System.Diagnostics;

namespace Poly.Solver
{
	[Serializable]
	[DebuggerDisplay("pos: {pos} vel: {vel} im: {invMass} gs: {gravityScale}")]
	public struct SolverNode
	{
		public Vec2 pos;

		public Vec2 vel;

		public float invMass;

		public float gravityScale;
	}
}
