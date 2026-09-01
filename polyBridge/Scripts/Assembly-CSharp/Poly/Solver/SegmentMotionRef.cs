namespace Poly.Solver
{
	public class SegmentMotionRef
	{
		public Vec2 angleToNode0;

		public Vec2 angleToNode1;

		public float comT;

		public float currentStretchedLength;

		public short worldIdx0;

		public short worldIdx1;

		public float lastConvertedAngleRef;

		public static implicit operator bool(SegmentMotionRef seg)
		{
			return seg != null;
		}
	}
}
