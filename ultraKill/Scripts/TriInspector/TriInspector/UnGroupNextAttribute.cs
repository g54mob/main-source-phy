using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class UnGroupNextAttribute : GroupNextAttribute
	{
		public UnGroupNextAttribute()
			: base(null)
		{
		}
	}
}
