using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class PropertyRangeAttribute : Attribute
	{
		public double Min;

		public double Max;

		public string MinMember;

		public string MaxMember;

		public PropertyRangeAttribute(double min, double max)
		{
			Min = ((min < max) ? min : max);
			Max = ((max > min) ? max : min);
		}

		public PropertyRangeAttribute(string minMember, double max)
		{
			MinMember = minMember;
			Max = max;
		}

		public PropertyRangeAttribute(double min, string maxMember)
		{
			Min = min;
			MaxMember = maxMember;
		}

		public PropertyRangeAttribute(string minMember, string maxMember)
		{
			MinMember = minMember;
			MaxMember = maxMember;
		}
	}
}
