using System.Collections.Generic;

namespace Poly.Math
{
	public static class Vec2Util
	{
		public static Vec2 CalcGeometricCenter(ICollection<Vec2> points)
		{
			Vec2 zero = Vec2.zero;
			foreach (Vec2 point in points)
			{
				zero += point;
			}
			return zero / points.Count;
		}
	}
}
