namespace Clipper2Lib
{
	public struct PointD
	{
		public double x;

		public double y;

		public PointD(PointD pt)
		{
			x = pt.x;
			y = pt.y;
		}

		public PointD(Point64 pt)
		{
			x = pt.X;
			y = pt.Y;
		}

		public PointD(Point64 pt, double scale)
		{
			x = (double)pt.X * scale;
			y = (double)pt.Y * scale;
		}

		public PointD(PointD pt, double scale)
		{
			x = pt.x * scale;
			y = pt.y * scale;
		}

		public PointD(long x, long y)
		{
			this.x = x;
			this.y = y;
		}

		public PointD(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public readonly string ToString(int precision = 2)
		{
			return string.Format($"{{0:F{precision}}},{{1:F{precision}}}", x, y);
		}

		public static bool operator ==(PointD lhs, PointD rhs)
		{
			if (InternalClipper.IsAlmostZero(lhs.x - rhs.x))
			{
				return InternalClipper.IsAlmostZero(lhs.y - rhs.y);
			}
			return false;
		}

		public static bool operator !=(PointD lhs, PointD rhs)
		{
			if (InternalClipper.IsAlmostZero(lhs.x - rhs.x))
			{
				return !InternalClipper.IsAlmostZero(lhs.y - rhs.y);
			}
			return true;
		}

		public override readonly bool Equals(object? obj)
		{
			if (obj != null && obj is PointD pointD)
			{
				return this == pointD;
			}
			return false;
		}

		public void Negate()
		{
			x = 0.0 - x;
			y = 0.0 - y;
		}

		public override readonly int GetHashCode()
		{
			return HashCode.Combine(x, y);
		}
	}
}
