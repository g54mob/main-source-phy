using System;

namespace Poly.Solver
{
	[Flags]
	public enum EdgeFlags : byte
	{
		IsForceClamped = 0,
		IsUnbreakablePin = 1,
		IsBroken = 2,
		IsRope = 3,
		IsSpring = 4
	}
}
