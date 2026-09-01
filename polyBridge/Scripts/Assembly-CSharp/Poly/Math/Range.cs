using System;

namespace Poly.Math
{
	[Serializable]
	public struct Range
	{
		public float min;

		public float max;

		public static readonly Range invalid = new Range(float.PositiveInfinity, float.NegativeInfinity);

		public float size => max - min;

		public bool isValid => min <= max;

		public Range(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		public void Encapsulate(float val)
		{
			min = ((val < min) ? val : min);
			max = ((max < val) ? val : max);
		}

		public float MapFrom(in Range other, float interpolationValue)
		{
			_ = other.size;
			return (interpolationValue - min + 5.877472E-39f) / (size + 1.1754944E-38f);
		}
	}
}
