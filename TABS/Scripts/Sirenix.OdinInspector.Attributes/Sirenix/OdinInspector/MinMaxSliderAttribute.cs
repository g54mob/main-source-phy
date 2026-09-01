using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class MinMaxSliderAttribute : Attribute
	{
		public float MinValue;

		public float MaxValue;

		public string MinMember;

		public string MaxMember;

		public string MinMaxMember;

		public bool ShowFields;

		public MinMaxSliderAttribute(float minValue, float maxValue, bool showFields = false)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			ShowFields = showFields;
		}

		public MinMaxSliderAttribute(string minMember, float maxValue, bool showFields = false)
		{
			MinMember = minMember;
			MaxValue = maxValue;
			ShowFields = showFields;
		}

		public MinMaxSliderAttribute(float minValue, string maxMember, bool showFields = false)
		{
			MinValue = minValue;
			MaxMember = maxMember;
			ShowFields = showFields;
		}

		public MinMaxSliderAttribute(string minMember, string maxMember, bool showFields = false)
		{
			MinMember = minMember;
			MaxMember = maxMember;
			ShowFields = showFields;
		}

		public MinMaxSliderAttribute(string minMaxMember, bool showFields = false)
		{
			MinMaxMember = minMaxMember;
			ShowFields = showFields;
		}
	}
}
