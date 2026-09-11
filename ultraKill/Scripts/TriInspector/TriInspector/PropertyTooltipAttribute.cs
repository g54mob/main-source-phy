using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class PropertyTooltipAttribute : Attribute
	{
		public string Tooltip { get; }

		public PropertyTooltipAttribute(string tooltip)
		{
			Tooltip = tooltip;
		}
	}
}
