using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public struct MMInterval<T> where T : struct, IComparable
	{
		public enum MMIntervalType
		{
			Inclusive = 0,
			Exclusive = 1
		}

		[Tooltip("the lower bound of this interval")]
		public T LowerBound;

		[Tooltip("the upper bound of this interval")]
		public T UpperBound;

		[Tooltip("whether to include or exclude the lower bound in the interval")]
		public MMIntervalType LowerBoundIntervalType;

		[Tooltip("whether to include or exclude the upper bound in the interval")]
		public MMIntervalType UpperBoundIntervalType;

		public MMInterval(T lowerBound, T upperBound, MMIntervalType lowerboundIntervalType = MMIntervalType.Inclusive, MMIntervalType upperboundIntervalType = MMIntervalType.Inclusive)
		{
			LowerBound = default(T);
			UpperBound = default(T);
			LowerBoundIntervalType = default(MMIntervalType);
			UpperBoundIntervalType = default(MMIntervalType);
		}

		public bool Contains(T value)
		{
			return false;
		}
	}
}
