using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class PropertySpaceAttribute : Attribute
	{
		public float SpaceBefore { get; set; }

		public float SpaceAfter { get; set; }

		public PropertySpaceAttribute()
		{
			SpaceBefore = 7f;
		}

		public PropertySpaceAttribute(float spaceBefore = 0f, float spaceAfter = 0f)
		{
			SpaceBefore = spaceBefore;
			SpaceAfter = spaceAfter;
		}
	}
}
