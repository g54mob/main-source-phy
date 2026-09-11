using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class EnableIfAttribute : DisableIfAttribute
	{
		public EnableIfAttribute(string condition)
			: this(condition, true)
		{
		}

		public EnableIfAttribute(string condition, object value)
			: base(condition, value)
		{
			base.Inverse = true;
		}
	}
}
