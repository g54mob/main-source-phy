using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowIfAttribute : HideIfAttribute
	{
		public ShowIfAttribute(string condition)
			: this(condition, true)
		{
		}

		public ShowIfAttribute(string condition, object value)
			: base(condition, value)
		{
			base.Inverse = true;
		}
	}
}
