using System.Runtime.CompilerServices;

namespace Poly.Geometry
{
	public struct Segment
	{
		public Vec2 v0;

		public Vec2 v1;

		public Vec2 normal
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vec2 result = v1 - v0;
				result.Rotate90();
				result.Normalize();
				return result;
			}
		}
	}
}
