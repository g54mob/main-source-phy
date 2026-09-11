using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class LabelWidthAttribute : Attribute
	{
		public float Width { get; }

		public LabelWidthAttribute(float width)
		{
			Width = width;
		}
	}
}
