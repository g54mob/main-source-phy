using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class DisableInEditModeAttribute : Attribute
	{
		public bool Inverse { get; protected set; }
	}
}
