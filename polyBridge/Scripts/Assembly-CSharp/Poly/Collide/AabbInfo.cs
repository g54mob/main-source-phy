using System.Runtime.CompilerServices;
using Poly.Physics;

namespace Poly.Collide
{
	public struct AabbInfo
	{
		public short minY;

		public short maxY;

		public short overlapIdx;

		public short collisionGroup;

		public Layer layer;

		public bool isTrigger;

		public short aabbIdx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return overlapIdx;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				overlapIdx = value;
			}
		}
	}
}
