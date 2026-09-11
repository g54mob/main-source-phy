using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class EnableInPlayModeAttribute : DisableInPlayModeAttribute
	{
		public EnableInPlayModeAttribute()
		{
			base.Inverse = true;
		}
	}
}
