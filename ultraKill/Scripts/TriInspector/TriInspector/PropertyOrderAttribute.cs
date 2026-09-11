using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class PropertyOrderAttribute : Attribute
	{
		public int Order { get; }

		public PropertyOrderAttribute(int order)
		{
			Order = order;
		}
	}
}
