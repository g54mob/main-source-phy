using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class TabAttribute : Attribute
	{
		public string TabName { get; }

		public TabAttribute(string tab)
		{
			TabName = tab;
		}
	}
}
