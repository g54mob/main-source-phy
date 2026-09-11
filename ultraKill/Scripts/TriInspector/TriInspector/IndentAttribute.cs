using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class IndentAttribute : Attribute
	{
		public int Indent { get; }

		public IndentAttribute()
			: this(1)
		{
		}

		public IndentAttribute(int indent)
		{
			Indent = indent;
		}
	}
}
